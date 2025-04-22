using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.Interfaces;
using DataAccessLayer.DTOs;
using MyBankApp.ViewModels;


namespace MyBankApp.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;
        [BindProperty]
        public CustomerDTO Customer { get; set; }
        public EditModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult OnGet(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return RedirectToPage("/CustomerDetails");
            }

            Customer = customer;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _customerService.UpdateCustomer(Customer.Id, Customer);
            return RedirectToPage("/Customers/Index");
        }
    }
}
