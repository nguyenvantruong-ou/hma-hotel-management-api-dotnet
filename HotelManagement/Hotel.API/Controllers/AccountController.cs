using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain.Accounts.Entity;
using Hotel.Domain.Accounts.Repository;
using Hotel.Infrastructure.Data;
using Hotel.SharedKernel.Email;
using Microsoft.AspNetCore.Mvc;
using NET.API.Controllers;
using System.IdentityModel.Tokens.Jwt;
using NET.Domain;
using System.Net;
using Hotel.API.Utils;
using System.Security.Claims;

namespace Hotel.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _repo;
        private readonly ITokenRegisterRepository _repoToken;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private IConfiguration _configuration;
        private IEmail _mailUtil;
        private CloudinaryUtil _cloudinaryUtil;

        public AccountController(IAccountRepository repo, 
                                ITokenRegisterRepository repoToken,
                                IUnitOfWork<HotelManagementContext> uow, 
                                IConfiguration Configuration,
                                IEmail Email)
        {
            _repo = repo;
            _repoToken = repoToken;
            _uow = uow;
            _configuration = Configuration;
            _mailUtil = Email;
            _mailUtil.ConfigMailAsync(_configuration);
            _cloudinaryUtil = new CloudinaryUtil(_configuration);
        }

        [HttpPost("auth/sign-up")]
        public async Task<ActionResult> SignUpAsync([FromBody] AccountRequestDTO Req)
        {
            try
            {
                if (await _repo.IsCardIdExist(Req.CardId))
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.CardIdExist));
                if (await _repo.IsEmailExist(Req.Email))
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.EmailExist));

                // send mail
                int Code;
                Random generator = new Random();
                Code = generator.Next(100000, 1000000);
                _mailUtil.SendAsync(Req.Email, _configuration["Hotel:Name"], Code + _configuration["Email:Content"]);

                
                Account Acc = new Account();
                Req.Password = MD5Util.GetMD5(Req.Password);
                Acc = ConvertAccount(Req);
                // upload image
                if (Req.File != null)
                    Acc.Avatar = _cloudinaryUtil.UploadToCloudinary(Req.File);
                Acc.RoleId = 3;
                Acc.Status = false;

                await _repo.AddEntityAsync(Acc);
                await _repoToken.AddEntityAsync(new TokenRegister(Req.Email.Trim(),JwtUtil.GetTokenRegister(_configuration, Code)));
                _uow.CompleteAsync();

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Req.Email, Message.Email));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpPost("auth/confirm")]
        public async Task<ActionResult> ConfirmAccountAsync([FromBody] ConfirmRequestDTO Req)
        {
            try
            {
                var TokenRegister = await _repoToken.GetTokenAsync(Req.Email);
                if (TokenRegister == null) 
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.OK, null, Message.CodeIncorrect));

                var Handler = new JwtSecurityTokenHandler();
                var TokenS = Handler.ReadToken(TokenRegister.Token) as JwtSecurityToken;

                int Code = int.Parse(TokenS.Claims.First(claim => claim.Type == ClaimTypesJwt.Code).Value);
                if(Req.Code == Code)
                {
                    await _repo.AccountActivated(Req.Email);
                    await _repoToken.DeleteAsync(Req.Email);
                    _uow.CompleteAsync();
                    return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.CodeCorrect));
                }
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.OK, null, Message.CodeIncorrect));
            } catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.OK, null, Message.Error, e.Message));
            }
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetAccountsAsync([FromQuery] string Kw)
        {
            //return Ok(_configuration["Email:Username"]);
            return Ok(_repo.GetEntityByName(Kw));
        }

        private Account ConvertAccount(AccountRequestDTO Req)
        {
            var Acc = new Account();
            Acc.Email = Req.Email;
            Acc.Password = Req.Password;
            Acc.FirstName = Req.FirstName;
            Acc.LastName = Req.LastName;   
            Acc.Gender = GetGender(Req.Gender);
            Acc.CardId = Req.CardId;
            Acc.PhoneNumber = Req.PhoneNumber;
            Acc.Address = Req.Address;

            return Acc;
        }

        private string GetGender(int type)
        {
            if (type == 1)
                return Gender.Male;
            return type ==2 ? Gender.Female : Gender.Other;
        }
    }
}

//var test = _cacheAccountRepository.GetEntityByName(req.Email);
//test.Select(_ => new
//{
//    _.Code,
//    _.Email,
//    _.Password,
//    _.FirstName,
//    _.LastName,
//    _.Gender,
//    _.Avatar,
//    _.CardId,
//    _.PhoneNumber,
//    _.Address
//})
//    .First();