using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/values")]
    [ApiController]
    [AllowAnonymous]
    public class ValuesController : ServiceController
    {
        public IActionResult Get()
        {
            return Ok("Ok");
        }
    }
}