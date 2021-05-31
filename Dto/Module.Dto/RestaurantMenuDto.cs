using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class RestaurantMenuDto : BaseDto<Guid>
    {
        public string Name { get; set; }
    }
}