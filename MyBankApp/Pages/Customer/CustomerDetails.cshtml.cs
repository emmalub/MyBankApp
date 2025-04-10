using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBankApp.ViewModels;
using Services.Services.Interfaces;

namespace MyBankApp.Pages.Customer
{
    [BindProperties]
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CustomerViewModel Customer { get; set; }

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult OnGet(int id)
        {
            var customer = _customerService.GetCustomerDetails(id);
            if (customer == null)
            {
                return RedirectToPage("/Customers");
            }

            Customer = new CustomerViewModel

            {
                Id = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Email = customer.Emailaddress,
                Phone = customer.Telephonecountrycode + " " + customer.Telephonenumber,
                Address = $"{customer.Streetaddress}, {customer.City}, {customer.Country}",
                Age = customer.Birthday != null ? DateTime.Now.Year - customer.Birthday.Value.Year : 0,
                Gender = customer.Gender,
                SocialSecurity = customer.NationalId,
                Accounts = customer.Dispositions
                .Where(d => d.Account != null)
                .Select(d => new AccountViewModel
                {
                    AccountId = d.Account.AccountId,
                    Balance = d.Account.Balance
                })
                .ToList(),
                TotalBalance = customer.Dispositions
                .Where(d => d.Account != null)
                .Sum(d => d.Account.Balance)
            };
            return Page();
        }
    }
}
