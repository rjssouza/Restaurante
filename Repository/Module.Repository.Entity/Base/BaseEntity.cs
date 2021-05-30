using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Repository.Entity.Base
{
    public class BaseEntity<TKey>
    {
        [Key]
        [Column("id")]
        public TKey Id { get; set; }
    }
}