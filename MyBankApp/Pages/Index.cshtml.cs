using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Repositories;
using DataAccessLayer.Models;

namespace MyBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;

        public int SwedishCustomerCount { get; set; }
        public int CustomerCount { get; set; }

        public IndexModel(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void OnGet()
        {
            CustomerCount = _customerRepository.GetCustomerCount();

            SwedishCustomerCount = _customerRepository.GetSwedishCustomerCount();
        }
    }
}
