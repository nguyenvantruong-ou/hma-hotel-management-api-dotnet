using Hotel.API.Areas.Management.DTOs.RequestDTO;
using Hotel.API.Areas.Management.Interfaces;
using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain.Rooms.Repository;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using NET.API.Controllers;
using NET.Domain;
using System.Net;

namespace Hotel.API.Areas.Management.Controllers
{
    public class RoomManagementController : BaseController
    {
        private readonly IRoomManagementRepository _repo;
        private readonly IImageManagementRepository _repoImage;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private readonly IRoomManagementService _service;
        public RoomManagementController(IRoomManagementRepository Repo,
                                        IUnitOfWork<HotelManagementContext> Uow,
                                        IRoomManagementService Service,
                                        IImageManagementRepository RepoImage)
        {
            _repo = Repo;
            _uow = Uow;
            _service = Service;
            _repoImage = RepoImage;
        }

        [HttpGet("rooms")]
        public async Task<ActionResult> ReadRooms([FromQuery] SearchRequestDTO Req)
        {
            var results = _repo.GetEntityByName(Req.Kw);
            return Ok(results.Skip(Req.PageSize * (Req.Page - 1)).Take(Req.PageSize).ToList());
        }

        [HttpGet("room/{Id}")]
        public async Task<ActionResult> ReadRoom(int Id)
        {
            try
            {
                var result = await _repo.GetEntityByIDAsync(Id);
                if (result == null)
                    return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.NotExist));
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, result , Message.Ok));
            } catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpDelete("room/{Id}")]
        public async Task<ActionResult> DeleteRoom(int Id)
        {
            try
            {
                await _repo.DeleteEntityAsync(Id);
                _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpPost("room")]
        public async Task<ActionResult> CreateRoom([FromForm] RoomRequestDTO Req)
        {
            try
            {
                if(await _repo.IsExistNameAsync(Req.RoomName))
                {
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.NameExist));
                }
                var Room = await _service.ConvertToRoomAsync(Req);
                Room.Slug = await _service.CreateSlug(Req.RoomName);
                await _repo.AddEntityAsync(Room);
                await _uow.CompleteAsync();
                var ListImages = await _service.UploadImageAsync(Req.ListImage);
                await _repoImage.AddListImageAsync(ListImages, await _repo.GetRoomIdLatestAsync());
                _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok)) ;
            } catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpPut("room")]
        public async Task<ActionResult> UpdateRoom([FromForm] RoomRequestDTO Req)
        {
            try
            {
                if (await _repo.IsExistNameByIdAsync(Req.RoomName, Req.Id))
                {
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, Message.NameExist));
                }

                if (Req.ListImage != null)
                { 
                    // images
                    await _repoImage.DeleteListImageAsync(Req.Id);
                    var ListImages = await _service.UploadImageAsync(Req.ListImage);
                    await _repoImage.AddListImageAsync(ListImages, Req.Id);
                }

                // update room
                var Room = await _service.ConvertToRoomAsync(Req);
                Room.Slug = await _service.CreateSlug(Req.RoomName);
                Room.Id = Req.Id;
                await _repo.UpdateEntityAsync(Room);
                _uow.CompleteAsync();

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK,Message.Ok));
            } catch(Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
