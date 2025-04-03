using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class TransactionDTO
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string? Type { get; set; } // T.ex. "Deposit", "Withdrawal"
        public decimal Balance { get; set; }
    }
}
