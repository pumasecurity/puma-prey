using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Puma.Prey.Rabbit.Models
{
    public class SafariUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SafariId { get; set; }
        public string PumaUserId { get; set; }
        [JsonIgnore]
        public Safari Safaris { get; set; }
        [JsonIgnore]
        public PumaUser PumaUser { get; set; }

    }
}
