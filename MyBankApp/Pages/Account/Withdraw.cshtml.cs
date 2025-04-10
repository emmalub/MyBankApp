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
            CustomerId = customerId;

            if (!ModelState.IsValid)
                return Page();

            var status = _transactionService.Withdraw(accountId, Amount);
            switch (status)
            {
                case ResponseCode.OK:
                    return RedirectToPage("/Customer", new { id = customerId });

                case ResponseCode.BalanceTooLow:
                    ModelState.AddModelError("Amount", "Can't be more than current balance!");
                    break;

                case ResponseCode.IncorrectAmount:
                    ModelState.AddModelError("Amount", "Amount must be between 100–10,000.");
                    break;

                case ResponseCode.AccountNotFound:
                    ModelState.AddModelError("Amount", "Account not found.");
                    break;

                // du kan lägga till fler fall här om du utökar ResponseCode senare
                default:
                    ModelState.AddModelError(string.Empty, "Something went wrong.");
                    break;
            }

            return Page();
        }
    }
}
