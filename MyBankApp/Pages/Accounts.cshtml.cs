using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBankApp.ViewModels;
using Services.Services;
using static MyBankApp.ViewModels.AccountViewModel;

namespace MyBankApp.Pages
{
    //Nu har du möjlighet att bestämma vilka sidor dina user roles ha tillgång till...
    //  På relevant sida lägg bara till koden:

    //   [Authorize(Roles = "Admin")]
    //eller...
    //   [Authorize(Roles = "Cashier")]
    [Authorize(Roles = "Cashier, Admin")]

    public class AccountsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public AccountsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } = 1;
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
        public int PageSize { get; set; } = 25;
        public string Q { get; set; }

        public void OnGet(string sortColumn = "Name", string sortOrder = "asc", int pageNo = 1, string q = "")
        {
            if (pageNo < 1)
                pageNo = 1;

            CurrentPage = pageNo;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            Q = q;

            var pagedResult = _accountService.GetSortedAccounts(SortColumn, SortOrder, q, pageNo, PageSize);

            Accounts = AccountMapper.MapToViewModel(pagedResult.Results);
            TotalPages = pagedResult.TotalPages;
        }
    }
}
