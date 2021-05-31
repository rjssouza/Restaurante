using Module.Repository.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Repository.Entity
{
    [Table("order_command")]
    public class OrderCommandEntity : BaseEntity<Guid>
    {
        [Column("client_table_id")]
        public Guid ClientTableId { get; set; }

        [Column("created_in")]
        public DateTime CreatedIn { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("paid")]
        public bool Paid { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, Valor {this.Price:C}";
        }
    }
}