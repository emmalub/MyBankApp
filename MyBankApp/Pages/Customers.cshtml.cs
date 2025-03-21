using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyBankApp.ViewModels;
using Services.Services;


namespace MyBankApp.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CustomersModel : PageModel
    {
        //private readonly BankAppDataContext _dbContext;
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } = 1;
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
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

            var query = _customerService.GetSortedCustomers(SortColumn, SortOrder, q);

            int totalCustomers = query.Count();
            TotalPages = (int)Math.Ceiling(totalCustomers / (double)PageSize);


            Customers = query
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .Select(c => new CustomerViewModel
                {
                    Id = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Country = c.Country,
                    City = c.City
                })
                .ToList();
        }
    }
}
