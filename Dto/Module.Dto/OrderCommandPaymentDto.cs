using System;
using Module.Dto.Base;

namespace Module.Dto
{
    public class OrderCommandPaymentDto : BaseDto<Guid>
    {
        public decimal Percentage { get; set; }
        public bool Paid { get; set; }
        public decimal Value { get; set; }
        public Guid OrderCommandId { get; set; }
    }
}