namespace Models
{
    public class StockMovement
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public double UnitaryValue { get; set; }

        public double SalePrice { get; set; } //preco do produto caso tenha um desconto
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Product Product { get; set; }
        public double MovementValue { get; set; } //antigo "profit"
        public int ProductId { get; set; }


        public Client Client { get; set; }
        public int? ClientID { get; set; }

        public StockMovement() { }
    }
}
