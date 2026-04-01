using Models;

namespace DataTransferObjects.StockMovement
{
    public class StockMovementGetDTO
    {
        public int Id { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public double UnitaryValue { get; set; }
        public double MovementValue { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public StockMovementGetDTO() { }
    }
}
