using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Services.Services.Interfaces;
using Microsoft.Identity.Client;

namespace MyBankApp.Pages.Account
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;
        public DepositModel(ITransactionService transactionService, IAccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [Range(100, 10000)]
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }

        //[Required(ErrorMessage = "Please add a comment")]
        //[MinLength(5, ErrorMessage = "Comments must be at least 5 characters long")]
        //[MaxLength(250, ErrorMessage = "Comment cant be more than 250 characters long")]
        //public string Comment { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            var account = _accountService.GetAccountDetails(accountId);

            if (account != null)
            {
                Balance = account.Balance;
                DepositDate = DateTime.Now;
            }
            else
            {
                Balance = 0;
            }
        }

        public IActionResult OnPost(int accountId)
        {
            var status = _transactionService.Deposit(accountId, Amount);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (status == ResponseCode.OK)
            {
                return RedirectToPage("/Customer/CustomerDetails", new { id = CustomerId });
            }

            if (status == ResponseCode.IncorrectAmount)
            {
                ModelState.AddModelError("Amount", "The amount must be between 100 and 10,000.");
            }
            else if (status == ResponseCode.AccountNotFound)
            {
                ModelState.AddModelError("Account", "The account was not found.");
            }

            return Page();
        }


        //public IActionResult OnPost(int accountId, int customerId)
        //{
        //    CustomerId = customerId;

        //    if (!ModelState.IsValid)
        //        return Page();

        //    var status = _transactionService.Deposit(accountId, Amount, Comment);

        //    switch (status)
        //    {
        //        case ResponseCode.OK:
        //            return RedirectToPage("/Customer", new { id = customerId });

        //        case ResponseCode.IncorrectAmount:
        //            ModelState.AddModelError("Amount", "The amount must be between 100 and 10,000.");
        //            break;

        //        case ResponseCode.CommentEmpty:
        //            ModelState.AddModelError("Comment", "Please add a comment.");
        //            break;

        //        case ResponseCode.AccountNotFound:
        //            ModelState.AddModelError(string.Empty, "The account was not found.");
        //            break;

        //        default:
        //            ModelState.AddModelError(string.Empty, "An unknown error occurred.");
        //            break;
        //    }
        //    return Page();
        //}

    }
}
