using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Repositories;
using DataAccessLayer.Models;
using Services.Repositories;


namespace MyBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CustomerRepository _customerRepository;
        private readonly AccountRepository _accountRepository;
        public IndexModel(CustomerRepository customerRepository, AccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }

        public int SwedishCustomerCount { get; set; }
        public int NorwegianCustomerCount { get; set; }
        public int DanishCustomerCount { get; set; }
        public int FinnishCustomerCount { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public int DanishAccountCount { get; set; }
        public int FinnishAccountCount { get; set; }
        public int SwedishAccountCount { get; set; }
        public int NorwegianAccountCount { get; set; }
        public decimal NorwegianCapital { get; set; }
        public decimal DanishCapital { get; set; }
        public decimal FinnishCapital { get; set; }
        public decimal SwedishCapital { get; set; }



        public void OnGet()
        {
            CustomerCount = _customerRepository.GetCustomerCount();
            AccountCount = _accountRepository.GetAccountCount();
            SwedishCustomerCount = _customerRepository.GetSwedishCustomerCount();
            NorwegianCustomerCount = _customerRepository.GetNorwegianCustomerCount();
            DanishCustomerCount = _customerRepository.GetDanishCustomerCount();
            FinnishCustomerCount = _customerRepository.GetFinnishCustomerCount();
            DanishAccountCount = _accountRepository.GetDanishAccountCount();
            FinnishAccountCount = _accountRepository.GetFinnishAccountCount();
            SwedishAccountCount = _accountRepository.GetSwedishAccountCount();
            NorwegianAccountCount = _accountRepository.GetNorwegianAccountCount();
            NorwegianCapital = _accountRepository.GetNorwegianCapital();
            DanishCapital = _accountRepository.GetDanishCapital();
            FinnishCapital = _accountRepository.GetFinnishCapital();
            SwedishCapital = _accountRepository.GetSwedishCapital();
        }    
    }
}
