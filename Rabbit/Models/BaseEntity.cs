using System;

using System.ComponentModel.DataAnnotations;

namespace Puma.Prey.Rabbit.Models
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}
