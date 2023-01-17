using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain;
using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IBillService _serviceBill;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        private IConfiguration _configuration;
        private ISendSMSService _serviceSMS;
        public PaymentController(IBillService serviceBill, IUnitOfWork<HotelManagementContext> uow, IConfiguration configuration
                                 , ISendSMSService serviceSMS)
        {
            _serviceBill = serviceBill;
            _uow = uow;
            _configuration = configuration;
            _serviceSMS = serviceSMS;
        }

        [Authorize(Roles = "ADMIN, STAFF, USER")]
        [HttpPost("payment-by-cash")]
        public async Task<ActionResult> CreateBill([FromBody] PaymentRequestDTO req)
        {
            try
            {
                await _serviceBill.CreateBillAsync(req.Id, req.StaffId, req.CostIncurred, req.TotalMoneyInOrder);
                await _uow.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }

        [HttpPost("payment-successful")]
        public async Task<ActionResult> SendSMS([FromBody] SendSMSRequestDTO req)
        {
            try
            {
                await _serviceSMS.TaskSendSMSServiceAsync(_configuration, req.PhoneNumber);
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, e.Message));
            }
        }
    }
}
