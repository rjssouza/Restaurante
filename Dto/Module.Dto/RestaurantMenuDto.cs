using System;
using System.Collections.Generic;
using Module.Dto.Base;

namespace Module.Dto
{
    public class RestaurantMenuDto : BaseDto<Guid>
    {
        public string Name { get; set; }

        public IEnumerable<RestaurantMenuItemDto> MenuList { get; set; }
    }
}