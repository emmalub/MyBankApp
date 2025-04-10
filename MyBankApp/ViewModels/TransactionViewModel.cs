namespace MyBankApp.ViewModels
{
    public class TransactionViewModel
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }
    }

}
