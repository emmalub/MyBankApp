using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _customerService.CreateCustomer(Customer);
                return RedirectToPage("/Customers/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the customer.");
                return Page();
            }
        }
    }
}
