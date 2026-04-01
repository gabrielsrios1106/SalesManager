namespace DataTransferObjects.Products
{
    public class ProductGetDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
            
        public int MinimumStock { get; set; }

        public int BalanceStock { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public DateTime CreatedAt { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public ProductGetDTO() { }
    }
}
