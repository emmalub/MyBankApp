using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.Services.Interfaces;

namespace MyBankApp.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;

        [BindProperty]
        public CustomerDTO Customer { get; set; } = new CustomerDTO();
        public CreateModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
      
       
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost method called");
          
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Givenname = Customer.Givenname,
                    Surname = Customer.Surname,
                    City = Customer.City,
                    Country = Customer.Country,
                    CountryCode = Customer.CountryCode,
                    Telephonecountrycode = Customer.Telephonecountrycode,
                    Telephonenumber = Customer.Telephonenumber,
                    Emailaddress = Customer.Emailaddress,
                    Streetaddress = Customer.Streetaddress,
                    Zipcode = Customer.Zipcode,
                    NationalId = Customer.NationalId,
                    Gender = Customer.Gender,
                    Birthday = Customer.Birthday,

                    IsActive = true
                };
                _customerService.CreateCustomer(Customer);

                TempData["SuccessMessage"] = "Customer has been created successfully!";

                var newCustomerId = Customer.Id;

                return RedirectToPage("/Customers/CustomerDetails", new { id = newCustomerId });

            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return Page();
        }
    }
}
