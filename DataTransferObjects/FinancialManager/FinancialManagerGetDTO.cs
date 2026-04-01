namespace DataTransferObjects.FinancialManager
{
    public class FinancialManagerGetDTO
    {
        public int Id { get; set; }
        public double GainSalesOfProduct { get; set; }
        public double LossOrExpenseOfProduct { get; set; }
        public double ProfitOfProduct { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string DepartmentName { get; set; }
        public byte Status { get; set; }

        public FinancialManagerGetDTO() { }
    }
}
