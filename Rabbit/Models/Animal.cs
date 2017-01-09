using System.ComponentModel.DataAnnotations;

namespace Puma.Prey.Rabbit.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual Hunt Hunt { get; set; }
    }
}