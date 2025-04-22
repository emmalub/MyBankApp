using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Services.Services.Interfaces;

namespace MyBankApp.Pages.Account
{
    public class TransferModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public TransferModel(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }
        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        [BindProperty]
        [Range(100, 10000, ErrorMessage = "Amount must be between 100-10,000.")]
        public decimal Amount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FromAccount { get; set; }

        [BindProperty]
        public int ToAccount { get; set; }

        public string Message { get; set; }
        public void OnGet(int fromAccount)
        {
            FromAccount = fromAccount;
            var account = _accountService.GetAccountDetails(fromAccount);
            if (account != null)
            {
                Balance = account.Balance;
            }
            else
            {
                ModelState.AddModelError("", "Account not found.");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
            //foreach (var error in ModelState)
            //{
            //    foreach (var subError in error.Value.Errors)
            //    {
            //        Console.WriteLine($"ModelState error on '{error.Key}': {subError.ErrorMessage}");
            //    }
            //}
                return Page();
            }

            var result = _transactionService.Transfer(FromAccount, ToAccount, Amount);
           
            if (result == ResponseCode.OK)
            {
                Message = "Transfer completed!";
                return RedirectToPage("/Customers/CustomerDetails", new { id = CustomerId });
            }

            if (result == ResponseCode.BalanceTooLow)
                ModelState.AddModelError("", "Insufficient balance.");
            
            else if (result == ResponseCode.AccountNotFound)
                ModelState.AddModelError("", "One or both accounts were not found.");
            
            else if (result == ResponseCode.SameAccount)
                ModelState.AddModelError("", "Cannot transfer to the same account.");
            
            else
            {
                ModelState.AddModelError("", "Transfer failed. Please try again.");
            }
                return Page();
        }
    }
}
