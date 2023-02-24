using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.Utils;
using Hotel.Domain.Feedbacks.Entities;
using Hotel.Domain.Feedbacks.Repositories;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hotel.API.Controllers;
using Hotel.Domain;
using static System.Net.WebRequestMethods;
using Hotel.Domain.Feedbacks.DomainServices.Interfaces;
using Hotel.API.DTOs.ResponseDTOs;
using System.Net;
using Hotel.API.DTOs.Constant;

namespace Hotel.API.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private readonly IFeedbackService _serviceFeedback;
        public FeedbackController(IFeedbackService serviceFeeback, 
                                  IUnitOfWork<HotelManagementContext> uow)
        {
            _serviceFeedback = serviceFeeback;
            _uow = uow;
        }

        [HttpPost("feedback")]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> AddFeedback([FromBody] FeedbackCreateRequestDTO req)
        {
            try
            {
                await _serviceFeedback.AddFeedbackAsync(req.UserId, req.Content.Trim(), req.Rating);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpGet("feedback-unread")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> CountUnreadFeedback()
        {
            try
            {
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, await _serviceFeedback.CountUnreadFeedbackAsync(), Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpGet("feeback-general")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> ReadFeebackGeneral([FromQuery] string? kw)
        {
            try
            {
                var results = _serviceFeedback.GetGeneralFeedbacks().OrderByDescending(s => s.DateCreated)
                    .Where(s => String.IsNullOrEmpty(kw) || (s.Account.LastName + " " + s.Account.FirstName).Contains(kw.Trim())).ToList();

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK,
                    results.Select(_ => new FeedbackGeneralResponseDTO(_, results.Where(s => s.AccountId == _.AccountId && s.IsRead == false).Count()))
                    .GroupBy(t => t.UserId).Select(grp => grp.Last()),
                    Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpGet("feedback/{userId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> ReadFeedback(int userId)
        {
            try
            {
                var results = _serviceFeedback.GetFeedbacks(userId);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, results, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
