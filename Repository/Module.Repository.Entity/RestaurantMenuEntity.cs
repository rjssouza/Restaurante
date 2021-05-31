using System;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("restaurant_menu")]
    public class RestaurantMenuEntity : BaseEntity<Guid>
    {
        [Column("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}