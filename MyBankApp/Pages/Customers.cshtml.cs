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
        public List<CustomerViewModel> Customers { get; set; }


        public void OnGet(string sortColumn, string sortOrder)
        {
            Customers = _customerService.GetSortedCustomers(sortColumn, sortOrder)
           .Take(50)
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
