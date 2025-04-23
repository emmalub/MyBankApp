using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace MyBankApp.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
        public decimal LoansTotal { get; set; }
        public decimal Transaction { get; set; }
        public DateOnly TransactionDate { get; set; }
        public int CurrentPage { get; set; }
        public bool HasMorePages { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new();
        public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    
    }
}
