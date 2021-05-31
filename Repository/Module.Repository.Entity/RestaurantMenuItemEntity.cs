using System;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("restaurant_menu_item")]
    public class RestaurantMenuItemEntity : BaseEntity<Guid>
    {
        [Column("number")]
        public int Number { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("price")]
        public decimal Price { get; set; }
        
        [Column("restaurant_menu_id")]
        public Guid RestaurantMenuId { get; set; }

        public override string ToString()
        {
            return $"{this.Number} - {this.Name}, Preço {this.Price.ToString("C")}";
        }
    }
}