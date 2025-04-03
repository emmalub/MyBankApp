using Azure.Core;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using MyBankApp.ViewModels;
using Services.Services;

namespace MyBankApp.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountDTO Account { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
        public int TotalTransactions { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } = 1;
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        private const int PageSize = 50;
        public string Q { get; set; }

        public AccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet(int accountId, string sortColumn = "Name", string sortOrder = "asc", int pageNo = 1, string q = "")
        {
            // Hämta kontoinformation
            Account = _accountService.GetAccountDetails(accountId);

            if (Account == null)
            {
                RedirectToPage("/Error");
                return;
            }

            if (pageNo < 1)
                pageNo = 1;
            CurrentPage = pageNo;

            SortColumn = sortColumn;
            SortOrder = sortOrder;

            if (Account.Transactions == null)
            {
                Transactions = new List<TransactionDTO>(); 
                return;
            }

            var transactionsQuery = Account.Transactions
    .Select(t => new TransactionDTO
    {
        Date = t.Date,
        Amount = t.Amount,
        Type = t.Type, 
        Balance = t.Balance,
    })
    .AsQueryable();

            // Hantera sortering
            if (!string.IsNullOrEmpty(sortColumn))
            {
                switch (sortColumn)
                {
                    case "Date":
                        transactionsQuery = (sortOrder == "asc") ? transactionsQuery.OrderBy(t => t.Date) : transactionsQuery.OrderByDescending(t => t.Date);
                        break;
                    case "Amount":
                        transactionsQuery = (sortOrder == "asc") ? transactionsQuery.OrderBy(t => t.Amount) : transactionsQuery.OrderByDescending(t => t.Amount);
                        break;
                    case "Type":
                        transactionsQuery = (sortOrder == "asc") ? transactionsQuery.OrderBy(t => t.Type) : transactionsQuery.OrderByDescending(t => t.Type);
                        break;
                }
            }

            // Paginerar resultaten
            Transactions = transactionsQuery
                .Skip((pageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public IActionResult OnGetMoreTransactions(int accountId, int pageNo)
        {
            // Load more transactions via AJAX
            var account = _accountService.GetAccountDetails(accountId);

            if (account == null)
                return NotFound();

            var transactions = account.Transactions
                .OrderByDescending(t => t.Date)
                .Skip((pageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return new JsonResult(transactions);
        }
        //    private readonly IAccountService _accountService;

        //    public AccountModel(IAccountService accountService)
        //    {
        //        _accountService = accountService;
        //    }
        //    public int CurrentPage { get; set; }
        //    public int TotalPages { get; set; } = 1;
        //    public string SortColumn { get; set; }
        //    public string SortOrder { get; set; }
        //    public List<AccountViewModel> Accounts { get; set; } = new();
        //    public int PageSize { get; set; } = 25;
        //    public string Q { get; set; }

        //    public void OnGet(int accountId, string sortColumn = "Name", string sortOrder = "asc", int pageNo = 1, string q = "")
        //    {

        //        Console.WriteLine($"Received Account ID: {accountId}");

        //        if (pageNo < 1)
        //            pageNo = 1;
        //        CurrentPage = pageNo;

        //        SortColumn = sortColumn;
        //        SortOrder = sortOrder;

        //        Q = q;

        //        var query = _accountService.GetSortedAccounts(SortColumn, SortOrder, q);

        //        int totalAccounts = query.Count();
        //        TotalPages = (int)Math.Ceiling(totalAccounts / (double)PageSize);


        //        Accounts = query
        //            .Skip((CurrentPage - 1) * PageSize)
        //            .Take(PageSize)
        //            .Select(a => new AccountViewModel
        //            {
        //                AccountId = a.AccountId,
        //                Balance = a.Balance,
        //                Transaction = a.Transaction,
        //                TransactionDate = a.TransactionDate,
        //                Created = a.Created,
        //            })
        //            .ToList();
        //    }
        //}
    }
}
