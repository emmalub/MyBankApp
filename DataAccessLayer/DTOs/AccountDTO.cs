using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
        public decimal LoansTotal { get; set; }
        public decimal Transaction { get; set; }
        public DateOnly TransactionDate { get; set; }
        public List<LoanDTO>? Loans { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }

  }
