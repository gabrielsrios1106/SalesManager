namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string CompleteName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public User() { }
    }
}
