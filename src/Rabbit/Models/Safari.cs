using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puma.Prey.Rabbit.Models
{
    public class Safari
    {
        public Safari()
        {
            Animals = new List<Animal>();
            Users = new List<SafariUser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Animal> Animals { get; set; }
        public List<SafariUser> Users { get; set; }

    }
}
