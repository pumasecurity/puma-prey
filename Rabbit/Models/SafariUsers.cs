namespace Puma.Prey.Rabbit.Models
{
    public class SafariUser
    {
        public int Id { get; set; }
        public int SafariId { get; set; }
        public Safari Safaris { get; set; }
        public string PumaUserId { get; set; }
        public PumaUser PumaUser { get; set; }

    }
}
