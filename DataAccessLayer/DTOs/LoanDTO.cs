using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? LoanType { get; set; }
        public decimal Payments { get; set; }
        public string? Status { get; set; }


    }
}
