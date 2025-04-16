using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class TransactionDTO
    {
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public string? Type { get; set; } 
        public decimal Balance { get; set; }
    }
}
