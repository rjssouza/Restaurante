using Module.Dto.Base;
using System;

namespace Module.Dto.ClientOrder
{
    /// <summary>
    /// Item da comanda do cliente
    /// </summary>
    public class ClientOrderItemDto : BaseDto<Guid>
    {
        /// <summary>
        /// Construtor do item, configura valor default quantidade
        /// </summary>
        public ClientOrderItemDto()
        {
            this.Quantity = 1;
        }

        /// <summary>
        /// Identificador item do cardapio
        /// </summary>
        public Guid MenuItemId { get; set; }

        /// <summary>
        /// Observação do cliente sobre o pedido
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// Quantidade, quantidade do item (default 1)
        /// </summary>
        public int Quantity { get; set; }
    }
}