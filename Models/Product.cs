namespace Models
{
    public class Product
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int MinimumStock { get; set; }

        public int BalanceStock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Department Department { get; set; }

        public int DepartmentId { get; set; }

        public byte Status { get; set; } = 1;

        public Product() { }
    }
}
