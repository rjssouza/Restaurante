using System;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("order_command")]
    public class OrderCommandItemEntity : BaseEntity<Guid>
    {
        [Column("observation")]
        public string Observation { get; set; }
        
        [Column("order_command_id")]
        public Guid OrderCommandId { get; set; }
        
        [Column("restaurant_menu_item_id")]
        public Guid RestaurantMenuItemId { get; set; }
        
        [Column("delivered")]
        public bool Delivered { get; set; }

        public override string ToString()
        {
            return $"{this.Observation}, Entregue {(this.Delivered ? "Sim" : "Nao")}";
        }
    }
}