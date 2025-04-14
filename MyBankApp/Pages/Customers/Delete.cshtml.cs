using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.Interfaces;
using Services.Services;
using Services.Repositories.Interfaces;
using Services.Repositories;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace MyBankApp.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerService _customerService;

        [BindProperty]
        public CustomerDTO Customer { get; set; }
        public int CustomerId { get; set; }

        public DeleteModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet(int id)
        {
            Customer = _customerService.GetCustomerById(id);
        }

        public IActionResult OnPost(int customerId)
        {
            try
            { 
                _customerService.DeleteCustomer(customerId);
                return RedirectToPage("/Customers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the customer.");
                return Page();
            }
        }
    }
}
