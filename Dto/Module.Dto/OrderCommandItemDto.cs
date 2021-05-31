using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class OrderCommandItemDto : BaseDto<Guid>
    {
        public string Observation { get; set; }
        public Guid OrderCommandId { get; set; }
        public Guid RestaurantMenuItemId { get; set; }
        public bool Delivered { get; set; }

    }
}