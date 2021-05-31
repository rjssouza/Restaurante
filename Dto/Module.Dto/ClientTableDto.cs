using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class ClientTableDto : BaseDto<Guid>
    {
        public int Number { get; set; }
    }
}