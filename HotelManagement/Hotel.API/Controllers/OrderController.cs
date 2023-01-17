using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain;
using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.Repositories;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.Entities;
using Hotel.Domain.Services.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _service;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private readonly IReadRoomService _serviceRoom;
        private readonly IOrderServiceRepository _repoOrderService;
        private readonly IBillService _serviceBill;

        public OrderController(IOrderService service,
                               IUnitOfWork<HotelManagementContext> uow,
                               IReadRoomService serviceRoom,
                               IOrderServiceRepository repoOrderService,
                               IBillRepository repoBill,
                               IBillService serviceBill)
        {
            _service = service;
            _uow = uow;
            _serviceRoom = serviceRoom;
            _repoOrderService = repoOrderService;
            _serviceBill = serviceBill;
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpGet("orders")]
        public async Task<ActionResult> ReadOrders([FromQuery] OrderReadRequestDTO req)
        {
            try
            {
                var results = await _service.ReadOrdersAsync(req.PhoneNumber);
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, results.Select(_ => new OrderResponseDTO(_)), Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpPost("order")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderRequestDTO req)
        {
            try
            {
                await _service.CreateOrderAsync(req.AccountId, req.CapitaId, req.StartDate, req.EndDate);
                await _uow.CompleteAsync();
                await _service.CreateOrderRoomAndServiceAsync(req.AccountId,req.RoomId, req.ServiceId);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpDelete("order/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                await _service.DeleteOrderAsync(id);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpGet("rooms-order")]
        public async Task<ActionResult> ReadRoomByDate([FromQuery] ReadRoomOrderRequestDTO req)
        {
            try
            {
                var results = await _service.ReadRoomsByDateAsync(req.StartDate, req.EndDate, req.CapitaId);
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, results.Select(_ => new RoomsHomeResponse(_)), Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpPost("total-money")]
        public async Task<ActionResult> GetTotalMoney([FromBody] TotalMoneyRequestDTO req)
        {
            try
            {
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, 
                                                await _service.GetTotalMoney(req.CapitaId, req.StartDate, req.EndDate, req.RoomId, req.ServiceId), 
                                                Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [Authorize(Roles = "STAFF, ADMIN")]
        [HttpGet("staff/orders")]
        public async Task<ActionResult> ReadOrdersByStaff([FromQuery] OrderReadByStaffRequestDTO req)
        {
            try
            {
                var result = _service.ReadOrdersByStaff()
                    .Where(s => String.IsNullOrEmpty(req.phoneNumber) || s.Account.PhoneNumber.Contains(req.phoneNumber));
                var orders = result.Skip(req.PageSize * (req.Page - 1)).Take(req.PageSize).ToList();

                List<OrderReadByStaffResponseDTO> results = new List<OrderReadByStaffResponseDTO>();
                orders.ForEach(async _ => 
                {
                    List<Room> rooms = await _serviceRoom.ReadRoomsHistoryAsync(_.Id);
                    List<Service> services = await _repoOrderService.ReadServicesHistoryAsync(_.Id);
                    decimal total;
                    if (_.IsPay == false)
                        total = await _service.GetTotalMoney((int)_.CapitaId, (DateTime)_.StartDate, (DateTime)_.EndDate,
                            rooms.Select(s => s.Id).ToList(), services.Select(s => s.Id).ToList());
                    else
                        total = (await _serviceBill.GetBillById(_.Id)).TotalMoney;
                    results.Add(new OrderReadByStaffResponseDTO( _, total));
                });

                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, results, 
                    (Math.Ceiling((double)result.Count() / req.PageSize)).ToString()));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
