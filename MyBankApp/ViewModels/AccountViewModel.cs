using DataAccessLayer.Models;

namespace MyBankApp.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
        public decimal LoansTotal { get; set; }
        public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}