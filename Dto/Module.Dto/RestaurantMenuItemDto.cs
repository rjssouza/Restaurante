using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class RestaurantMenuItemDto : BaseDto<Guid>
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid RestaurantMenuId { get; set; }

    }
}