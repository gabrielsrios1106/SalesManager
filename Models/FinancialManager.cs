namespace Models
{
    public class FinancialManager
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public double GainSalesOfProduct { get; set; }
        public double LossOrExpenseOfProduct { get; set; }
        public double ProfitOfProduct { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public FinancialManager() { }
    }
}
