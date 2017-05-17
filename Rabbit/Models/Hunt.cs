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

        public virtual ApplicationUser PrimaryContact { get; set; }

        public virtual List<Animal> Animals { get; set; }
    }
}