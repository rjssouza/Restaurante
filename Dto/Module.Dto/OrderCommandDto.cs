using Module.Dto.Base;
using System;

namespace Module.Dto
{
    public class OrderCommandDto : BaseDto<Guid>
    {
        public Guid ClientTableId { get; set; }
        public DateTime CreatedIn { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool Paid { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
    }
}