using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBankApp.ViewModels;
using Services.Services;

namespace MyBankApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public CustomerViewModel Customer { get; set; }


        public void OnGet(int customerId)
        {
            var customer = _customerService.GetCustomerDetails(customerId);
            if (customer == null)
            {
                RedirectToPage("/Customers");
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
            return;
        }
    }
}
