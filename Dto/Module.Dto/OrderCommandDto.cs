using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class OrderCommandDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ClientTableId { get; set; }
        public bool Paid { get; set; }
        public decimal Value { get; set; }
    }
}