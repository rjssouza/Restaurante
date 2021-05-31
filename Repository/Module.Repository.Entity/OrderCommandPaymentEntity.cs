using System;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("order_command_payment")]
    public class OrderCommandPaymentEntity : BaseEntity<Guid>
    {
        [Column("percentage")]
        public decimal Percentage { get; set; }
        
        [Column("paid")]
        public bool Paid { get; set; }
        
        [Column("value")]
        public decimal Value { get; set; }
        
        [Column("order_command_id")]
        public Guid OrderCommandId { get; set; }

        public override string ToString()
        {
            return $"Porcentagem: {this.Percentage.ToString("P")}, Valor: {this.Value.ToString("C")}";
        }
    }
}