using System;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("order_command")]
    public class OrderCommandEntity : BaseEntity<Guid>
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("client_table_id")]
        public Guid ClientTableId { get; set; }
        
        [Column("paid")]
        public bool Paid { get; set; }
        
        [Column("value")]
        public decimal Value { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, Valor {this.Value.ToString("C")}";
        }
    }
}