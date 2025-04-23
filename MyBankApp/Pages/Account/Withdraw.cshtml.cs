using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models;
using Services.Services.Interfaces;


namespace MyBankApp.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public WithdrawModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }


        [Range(100, 10000)]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SuccessMessage { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            var account = _accountService.GetAccountDetails(accountId);
            CustomerId = customerId;

            if (account != null)
            {
                Balance = account.Balance;
            }
            else
            {
                Balance = 0;
            }
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            var status = _transactionService.Withdraw(accountId, Amount);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (status == ResponseCode.OK)
            {
                return RedirectToPage("/Account/Withdraw", new { accountId = accountId, customerId = CustomerId, successMessage = "Withdraw successful!" });
            }

            if (status == ResponseCode.IncorrectAmount)
            {
                ModelState.AddModelError("Amount", "The amount must be between 100 and 10,000.");
            }
            if (status == ResponseCode.BalanceTooLow)
            {
                ModelState.AddModelError("Amount", "The amount is higher than the accounts balance.");
            }
            else if (status == ResponseCode.AccountNotFound)
            {
                ModelState.AddModelError("Account", "The account was not found.");
            }

            return Page();
        }
    }
}
