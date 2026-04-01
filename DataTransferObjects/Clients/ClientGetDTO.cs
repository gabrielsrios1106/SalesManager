namespace DataTransferObjects.Clients
{
    public class ClientGetDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientAddressState { get; set; }
        public string ClientAddressCity { get; set; }
        public string ClientCEP { get; set; }
        public string ClientEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public ClientGetDTO() { }
    }
}
