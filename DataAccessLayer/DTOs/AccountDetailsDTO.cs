using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class AccountDetailsDTO
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
    }

  }
