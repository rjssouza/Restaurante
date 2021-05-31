using Module.Dto.Base;
using System;
using System.Collections.Generic;

namespace Module.Dto.ClientOrder
{
    /// <summary>
    /// Status da comanda do cliente
    /// </summary>
    public class ClientOrderStatusDto : BaseDto<Guid>
    {
        /// <summary>
        /// Status da comanda
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Itens que foram pedidos pelo cliente
        /// </summary>
        public IEnumerable<OrderCommandItemDto> OrderCommandItemList { get; set; }
    }
}