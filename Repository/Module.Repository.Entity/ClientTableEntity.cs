using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Module.Repository.Entity.Base;

namespace Module.Repository.Entity
{
    [Table("client_table")]
    public class ClientTableEntity : BaseEntity<Guid>
    {
        [Column("number")]
        [Required(ErrorMessage = "É obrigatório informar o número da mesa")]
        public int Number { get; set; }

        public override string ToString()
        {
            return $"Mesa número {this.Number}";
        }
    }
}