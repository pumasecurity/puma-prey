using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Puma.Prey.Rabbit.Models
{
    public class Safari
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Animal> Animals { get; set; }      
        public List<SafariUser> SafariUsers { get; set; }

    }
}
