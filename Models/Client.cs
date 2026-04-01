using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Client
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddressState { get; set; }
        public string ClientAddressCity { get; set; }
        public string ClientCEP { get; set; }
        public string ClientEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public byte Status { get; set; } = 1;

        public Client() { }
    }
}
