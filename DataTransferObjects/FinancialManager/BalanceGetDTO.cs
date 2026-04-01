using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.FinancialManager
{
    public class BalanceGetDTO
    {
        public double AllProfit { get; set; }
        public double AllGain { get; set; }
        public double AllExpenseOrLoss { get; set; }

        public BalanceGetDTO() { }
    }
}
