using Azure.Core;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using MyBankApp.ViewModels;
using Services.Services.Interfaces;

namespace MyBankApp.Pages.Account
{
    public class AccountIndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public AccountDTO Account { get; set; }
        public int TotalTransactions { get; set; }
        public int CurrentPage { get; set; }
        public bool HasMorePages { get; set; }
        public int TotalPages { get; set; } = 1;
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        private const int PageSize = 20;
        public List<TransactionDTO> Transactions { get; set; } = new();

        public AccountIndexModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public void OnGet(int accountId, string sortColumn = "Date", string sortOrder = "asc", int pageNo = 1)
        {
            Account = _accountService.GetAccountDetails(accountId);
            SortColumn = sortColumn;
            SortOrder = sortOrder;


            if (Account == null)
            {
                RedirectToPage("/Error");
                return;
            }

            CurrentPage = pageNo < 1 ? 1 : pageNo;
            TotalTransactions = Account.Transactions.Count();

            var transactionsQuery = _transactionService.GetSortedTransactions(accountId, sortColumn, sortOrder);
            var paged = _transactionService.GetTransactionsByAccount(accountId, pageNo, PageSize);

            Transactions = paged.Results;
            HasMorePages = pageNo < paged.PageCount;
            CurrentPage = paged.CurrentPage;
            TotalPages = paged.PageCount;
        }

    
        public JsonResult OnGetTransactions(int accountId, int pageNo)
        {
            var paged = _transactionService.GetTransactionsByAccount(accountId, pageNo, 20);

            return new JsonResult(new
            {
                transactions = paged.Results,
                hasMorePages = pageNo < paged.PageCount
            });
        }
    }
}