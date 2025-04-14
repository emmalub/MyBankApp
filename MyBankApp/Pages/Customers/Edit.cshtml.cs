using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.Interfaces;
using DataAccessLayer.DTOs;


namespace MyBankApp.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;
        [BindProperty]
        public CustomerDTO Customer { get; set; }
        public int CustomerId { get; set; }
        public EditModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public void OnGet(int id)
        {
            Customer = _customerService.GetCustomerById(id);
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _customerService.UpdateCustomer(CustomerId, Customer);
            return RedirectToPage("/Customers/Index");
        }
    }
}
