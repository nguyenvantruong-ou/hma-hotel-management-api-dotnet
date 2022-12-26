using Hotel.API.DTOs.RequestDTOs;
using Hotel.API.Utils;
using Hotel.Domain.Feedbacks.Entity;
using Hotel.Domain.Feedbacks.Repository;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NET.API.Controllers;
using NET.Domain;
using static System.Net.WebRequestMethods;

namespace Hotel.API.Controllers
{
    public class FeedbackController : BaseController
    {
        private IFeedbackRepository _repo;
        private readonly IUnitOfWork<HotelManagementContext> _uow;
        public FeedbackController(IFeedbackRepository Repo, 
                                  IUnitOfWork<HotelManagementContext> uow)
        {
            this._repo = Repo;
            _uow = uow;
        }

        [HttpPost]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> AddFeedback([FromBody] FeedbackCreateRequestDTO Req)
        {

            //var Input = new Feedback();
            //Input.Content = Req.Content;
            //await _repo.AddEntityAsync(Input);
            //_uow.CompleteAsync();
            return Ok();
        }
    }
}
