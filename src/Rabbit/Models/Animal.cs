using System;
using System.ComponentModel.DataAnnotations;

namespace Puma.Prey.Rabbit.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public int SafariId { get; set; }
        [Required]
        public string AnimalName { get; set; }
        public string Species { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Safari Safari { get; set; }
    }
}