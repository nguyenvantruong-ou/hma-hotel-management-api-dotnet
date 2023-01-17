using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Rooms.Repositories;
using Hotel.Domain.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.API.Controllers
{
    public class HistoryController : BaseController
    {
        private readonly IHistoryService _service;
        private readonly IReadRoomService _serviceRoom;
        private readonly IOrderServiceRepository _repoOrderService;
        private readonly IOrderService _serviceOrder;

        public HistoryController(IHistoryService service,
                                 IReadRoomService serviceRoom,
                                 IOrderServiceRepository repoOrderService,
                                 IOrderService serviceOrder)
        {
            _service = service;
            _serviceRoom = serviceRoom;
            _repoOrderService = repoOrderService;
            _serviceOrder = serviceOrder;
        }

        [Authorize(Roles = "USER")]
        [HttpGet("history/{accId}")]
        public async Task<ActionResult> ReadHistory(int accId)
        {
            try
            {
                var orders = _service.ReadOrderByUserIDAsync(accId).ToList();

                List<HistoryResponseDTO> resutls = new List<HistoryResponseDTO>();

                orders.ForEach(async s =>
                {
                    List<Room> rooms = await _serviceRoom.ReadRoomsHistoryAsync(s.Id);
                    List<Service> services = await _repoOrderService.ReadServicesHistoryAsync(s.Id);

                    List<RoomsHomeResponse> roomsHomeResponses = new List<RoomsHomeResponse>();
                    rooms.ForEach(_ => roomsHomeResponses.Add(new RoomsHomeResponse(_)));

                    decimal total = await _serviceOrder.GetTotalMoney((int)s.CapitaId, (DateTime)s.StartDate, (DateTime)s.EndDate, 
                        rooms.Select(s => s.Id).ToList(), services.Select(s=> s.Id).ToList());

                    resutls.Add(new HistoryResponseDTO(s.Id, s.Account.LastName + " " + s.Account.FirstName,
                                                       s.Capita.AmountOfPeople, s.Status, s.IsPay, total, s.DateCreated, s.StartDate,
                                                       s.EndDate, roomsHomeResponses, services));
                });

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, resutls, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
