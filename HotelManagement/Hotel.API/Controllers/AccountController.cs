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
using Microsoft.AspNetCore.Authorization;
using Hotel.API.Interfaces.Services;
using Hotel.API.Interfaces.Utils;

namespace Hotel.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _repo;
        private readonly ITokenRegisterRepository _repoToken;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private IConfiguration _configuration;
        private IEmail _mailUtil;
        private ICloudinary _cloudinaryUtil;
        private IAccountService _service;

        public AccountController(IAccountRepository Repo, 
                                ITokenRegisterRepository repoToken,
                                IUnitOfWork<HotelManagementContext> Uow, 
                                IConfiguration Configuration,
                                IEmail Email,
                                IAccountService Service,
                                ICloudinary cloudinaryUtil)
        {
            _repo = Repo;
            _repoToken = repoToken;
            _uow = Uow;
            _configuration = Configuration;
            _mailUtil = Email;
            _mailUtil.ConfigMailAsync(_configuration);
            _cloudinaryUtil = cloudinaryUtil;
            _service = Service;
        }

        [HttpPost("auth/sign-up")]
        public async Task<ActionResult> SignUpAsync([FromForm] AccountRequestDTO Req)
        {
            try
            {
                if (await _repo.IsCardIdExistAsync(Req.CardId))
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.CardIdExist));
                if (await _repo.IsEmailExistAsync(Req.Email))
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.EmailExist));

                // send mail
                int Code;
                Random generator = new Random();
                Code = generator.Next(100000, 1000000);
                _mailUtil.SendAsync(Req.Email, _configuration["Hotel:Name"], Code + _configuration["Email:Content"]);

                
                Account Acc = new Account();
                Req.Password = MD5Util.GetMD5(Req.Password);
                Acc = _service.ConvertAccount(Req);
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
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.OK, Message.CodeIncorrect));

                var Handler = new JwtSecurityTokenHandler();
                var TokenS = Handler.ReadToken(TokenRegister.Token) as JwtSecurityToken;

                int Code = int.Parse(TokenS.Claims.First(claim => claim.Type == ClaimTypesJwt.Code).Value);
                if(Req.Code == Code)
                {
                    await _repo.AccountActivatedAsync(Req.Email);
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

 
        [HttpPost("auth/sign-in")]
        public async Task<ActionResult> SignIn([FromBody] LoginRequestDTO Req )
        {
            try
            {
                var result = await _repo.SignInAsync(Req.Email, MD5Util.GetMD5(Req.Password));

                return Ok(result != null ? 
                        new CommonResponseDTO((int)HttpStatusCode.OK, 
                            new SignInResponseDTO(result.Email, result.LastName +" " + result.FirstName, result.Avatar, 
                            JwtUtil.GetToken(_configuration, result)), 
                        Message.Ok)
                    : new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Incorrect));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, e.Message));
            }
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