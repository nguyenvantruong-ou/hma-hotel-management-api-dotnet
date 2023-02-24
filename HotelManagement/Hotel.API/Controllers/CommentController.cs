using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.API.Controllers
{
    public class CommentController : BaseController
    {
        private ICreateCommentService _serviceCreateComment; 
        private IReadCommentService _serviceReadComment;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private IDeleteCommentService _serviceDeleteComment;
        private IUpdateCommentService _serviceUpdateComment;
        public CommentController(ICreateCommentService serviceCreateComment,
                                 IReadCommentService serviceReadComment,
                                 IUnitOfWork<HotelManagementContext> uow,
                                 IDeleteCommentService serviceDeleteComment,
                                 IUpdateCommentService serviceUpdateComment)
        {
            _serviceCreateComment = serviceCreateComment;
            _serviceReadComment = serviceReadComment;
            _uow = uow;
            _serviceDeleteComment = serviceDeleteComment;
            _serviceUpdateComment = serviceUpdateComment;
        }

        [HttpGet("comments")]
        public async Task<ActionResult> ReadComment([FromQuery] CommentReadRequestDTO req)
        {
            try
            {
                var listComments = await _serviceReadComment.ReadCommentAsync(req.RoomId, req.ToIndex);
                CommentResponseDTO results = new CommentResponseDTO();
                results.AmountComment = await _serviceReadComment.CountCommentAsync(req.RoomId);
                listComments.ForEach(_ =>
                {
                    results.ListComment.Add(new CommentInfoResponseDTO(_));
                });

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, results, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpPost("permission")]
        public async Task<ActionResult> Check([FromBody] CheckPermissionComment req)
        {
            try
            {
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, 
                                                await _serviceCreateComment.CheckPermission(req.RoomId, req.UserId), 
                                                Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "USER")]
        [HttpPost("comment")]
        public async Task<ActionResult> CreateComment([FromBody] CommentCreateRequestDTO req)
        {
            try
            {
                await _serviceCreateComment.CreateCommentAsync(req.AccountId, req.RoomId, req.Content, req.Incognito, req.ParentId);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "USER")]
        [HttpDelete("comment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                await _serviceDeleteComment.DeleteCommentAsync(id);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "USER")]
        [HttpPut("comment")]
        public async Task<ActionResult> UpdateComment([FromBody] CommentUpdateRequestDTO req)
        {
            try
            {
                //this.HttpContext.User.FindFirst("sub").Value;
                await _serviceUpdateComment.UpdateCommentAsync(req.Id, req.Content, req.Incognito);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
