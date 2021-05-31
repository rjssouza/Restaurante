using System;
using System.Collections.Generic;

namespace Module.Dto.ClientOrder
{
    /// <summary>
    /// Comanda do cliente 
    /// </summary>
    public class ClientOrderDto
    {
        /// <summary>
        /// Identificador da mesa do cliente que esta solicitando a comanda
        /// </summary>
        public Guid ClientTableId { get; set; }

        /// <summary>
        /// Observações sobre o pedido
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Lista de itens da comanda
        /// </summary>
        public List<ClientOrderItemDto> ItemList { get; set; }
    }
}