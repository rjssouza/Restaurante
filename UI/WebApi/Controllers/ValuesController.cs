using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Dto;
using Module.Service.Interface;

namespace WebApi.Controllers
{
    [Route("api/values")]
    [ApiController]
    [AllowAnonymous]
    public class ValuesController : ServiceController
    {
        public IMenuService MenuService { get; set; }
        
        [ProducesResponseType(200, Type = typeof(ClientMenuViewDto))]
        public IActionResult Get()
        {
            var clientMenuView = this.MenuService.GetMenuView();
            
            return Ok(clientMenuView);
        }
    }
}