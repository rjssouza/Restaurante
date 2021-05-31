using Module.Dto.Base;
using System;

namespace Module.Dto
{
    /// <summary>
    /// Item de comanda do cliente
    /// </summary>
    public class OrderCommandItemDto : BaseDto<Guid>
    {
        /// <summary>
        /// Entregue
        /// </summary>
        public bool Delivered { get; set; }

        /// <summary>
        /// Observações do cliente
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// Identificador da comanda
        /// </summary>
        public Guid OrderCommandId { get; set; }

        /// <summary>
        /// Item do menu do restaurante
        /// </summary>
        public Guid RestaurantMenuItemId { get; set; }
    }
}