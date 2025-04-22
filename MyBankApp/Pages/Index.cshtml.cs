using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.Interfaces;


namespace MyBankApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStatisticsService _statisticsService;

        public IndexModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public int SwedishCustomerCount { get; set; }
        public int NorwegianCustomerCount { get; set; }
        public int DanishCustomerCount { get; set; }
        public int FinnishCustomerCount { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public int UserCount { get; set; }
        public decimal CapitalCount { get; set; }
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
            CustomerCount = _statisticsService.GetTotalCustomers();
            AccountCount = _statisticsService.GetTotalAccounts();
            CapitalCount = _statisticsService.GetTotalCapital();
            UserCount = _statisticsService.GetTotalUserCount();

            SwedishCustomerCount = _statisticsService.GetSwedishCustomerCount();
            NorwegianCustomerCount = _statisticsService.GetNorwegianCustomerCount();
            DanishCustomerCount = _statisticsService.GetDanishCustomerCount();
            FinnishCustomerCount = _statisticsService.GetFinnishCustomerCount();

            SwedishAccountCount = _statisticsService.GetSwedishAccountCount();
            NorwegianAccountCount = _statisticsService.GetNorwegianAccountCount();
            DanishAccountCount = _statisticsService.GetDanishAccountCount();
            FinnishAccountCount = _statisticsService.GetFinnishAccountCount();

            SwedishCapital = _statisticsService.GetSwedishCapital();
            NorwegianCapital = _statisticsService.GetNorwegianCapital();
            DanishCapital = _statisticsService.GetDanishCapital();
            FinnishCapital = _statisticsService.GetFinnishCapital();

        }    
    }
}
