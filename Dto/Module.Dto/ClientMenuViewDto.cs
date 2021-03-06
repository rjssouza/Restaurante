using System;
using System.Collections.Generic;
using Module.Dto.Base;

namespace Module.Dto
{
    public class ClientMenuViewDto : BaseDto<Guid>
    {
        public RestaurantMenuDto RestaurantMenu { get; set; }

        public IEnumerable<GenericSelectDto<Guid>> ClientTableSelectList { get; set; }
    }
}