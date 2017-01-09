using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Puma.Prey.Rabbit.Models
{
    public class Hunt
    {
        public Hunt()
        {
            this.Animals = new List<Animal>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public virtual List<Animal> Animals { get; set; }
    }
}