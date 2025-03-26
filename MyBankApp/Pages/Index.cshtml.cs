using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Repositories;
using DataAccessLayer.Models;

namespace MyBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        public IndexModel(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int SwedishCustomerCount { get; set; }
        public int NorwegianCustomerCount { get; set; }
        public int DanishCustomerCount { get; set; }
        public int FinnishCustomerCount { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }


        public void OnGet()
        {
            CustomerCount = _customerRepository.GetCustomerCount();
            AccountCount = _customerRepository.GetAccountCount();
            SwedishCustomerCount = _customerRepository.GetSwedishCustomerCount();
            NorwegianCustomerCount = _customerRepository.GetNorwegianCustomerCount();
            DanishCustomerCount = _customerRepository.GetDanishCustomerCount();
            FinnishCustomerCount = _customerRepository.GetFinnishCustomerCount();
        }
    }
}
