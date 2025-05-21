using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Puma.Prey.Rabbit.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SafariId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Species { get; set; }
        public string Weight { get; set; }
        public string Color { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [JsonIgnore]
        public Safari Safari { get; set; }
    }
}