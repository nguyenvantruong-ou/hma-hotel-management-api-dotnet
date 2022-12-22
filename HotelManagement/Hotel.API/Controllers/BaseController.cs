using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NET.Domain;

namespace NET.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
    }
}
