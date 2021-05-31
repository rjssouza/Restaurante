using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Dto;
using Module.Service.Interface;

namespace WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/values")]
    [ApiController]
    [AllowAnonymous]
    public class ClientMenuController : ServiceController
    {
        /// <summary>
        /// 
        /// </summary>
        public IClientMenuService MenuService { get; set; }

        /// <summary>
        /// Obtém o menu digital para a escolha do cliente
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ClientMenuViewDto))]
        public IActionResult GetMenuView()
        {
            var clientMenuView = this.MenuService.GetMenuView();
            
            return Ok(clientMenuView);
        }
    }
}