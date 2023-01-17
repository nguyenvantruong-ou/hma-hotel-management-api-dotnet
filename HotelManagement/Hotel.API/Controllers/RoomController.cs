using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Hotel.API.Controllers;
using Hotel.Domain;
using Hotel.API.DTOs.ResponseDTOs;
using System.Net;
using Hotel.API.DTOs.Constant;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.API.DTOs.RequestDTOs;

namespace Hotel.API.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IReadRoomService _serviceRoom;
        private readonly IReadCommentService _serviceComment;
        public RoomController(IReadRoomService serviceRoom, IReadCommentService serviceComment)
        {
            _serviceRoom = serviceRoom;
            _serviceComment = serviceComment;
        }

        [HttpGet("rooms")]
        public async Task<ActionResult> ReadRooms([FromQuery] SearchPagingRequestDTO req)
        {
            try
            {

                var results = await _serviceRoom.ReadRoomsAsync(req.Kw, req.PageSize, req.Page, req.Sort);
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK,
                    new PagingResponseDTO(await _serviceRoom.GetPageMaxAsync(req.Kw, req.PageSize), results.Select(_=>new RoomsHomeResponse(_))),
                    Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpGet("room/{id}")]
        public async Task<ActionResult> ReadRoom(int id)
        {
            try
            {
                var result = new RoomDetailResponseDTO(await _serviceRoom.ReadRoomAsync(id));
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, result, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
