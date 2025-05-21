using System;
using System.ComponentModel.DataAnnotations;

namespace Coyote.Models.Animal
{
    public class AnimalRequest
    {
        public int Id { get; set; }

        [Required]
        public int SafariId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Weight { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }
    }
}