using Hotel.API.DTOs.Constant;
using Hotel.API.DTOs.ResponseDTOs;
using Hotel.Domain.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotel.API.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceRepository _repo;
        public ServiceController(IServiceRepository repo) 
        {
            _repo = repo;
        }

        [HttpGet("services")]
        public async Task<ActionResult> ReadService()
        {
            try
            {
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, _repo.GetEntityByName("").ToList(), Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null, Message.Error, e.Message));
            }
        }
    }
}
