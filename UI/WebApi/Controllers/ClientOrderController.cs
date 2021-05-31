using Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Dto.ClientOrder;
using Module.Service.Interface;

namespace WebApi.Controllers
{
    /// <summary>
    /// Serviço para comanda de clientes
    /// </summary>
    [Route("api/client-order")]
    [ApiController]
    [AllowAnonymous]
    public class ClientOrderController : ServiceController
    {
        /// <summary>
        /// Client order service
        /// </summary>
        public IClientOrderService ClientOrderService { get; set; }

        /// <summary>
        /// Criar a comanda para mesa do cliente
        /// </summary>
        /// <param name="clientOrderDto"></param>
        /// <returns>Número da comanda</returns>
        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult CreateOrder(ClientOrderDto clientOrderDto)
        {
            var orderNumber = this.ClientOrderService.CreateOrder(clientOrderDto);

            return Ok(orderNumber);
        }

        /// <summary>
        /// Consulta status da comanda do cliente
        /// </summary>
        /// <param name="orderNumber">Número da comanda</param>
        /// <returns>Status da ordem do cliente</returns>
        [HttpGet("{orderNumber}")]
        [ProducesResponseType(200, Type = typeof(ClientOrderStatusDto))]
        public IActionResult GetStatusOrder(int orderNumber)
        {
            var clientStatusOrder = this.ClientOrderService.GetStatusOrder(orderNumber);

            return Ok(clientStatusOrder);
        }
    }
}