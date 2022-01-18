using System;

namespace Puma.Prey.Rabbit.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public int SafariId { get; set; }
        public string AnimalName { get; set; }
        public string Species { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Safari Safari { get; set; }
    }
}