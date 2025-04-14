using Services.Repositories.Interfaces;
using Services.Services.Interfaces;



namespace Services.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ICustomerRepository _customerRepo;

        public StatisticsService(IAccountRepository accountRepo, ICustomerRepository customerRepo)
        {
            _accountRepo = accountRepo;
            _customerRepo = customerRepo;
        }

        public int GetTotalCustomers() => _customerRepo.GetCustomerCount();
        public int GetSwedishCustomerCount() => _customerRepo.GetCustomerCountByCountry("Sweden");
        public int GetNorwegianCustomerCount() => _customerRepo.GetCustomerCountByCountry("Norway");
        public int GetDanishCustomerCount() => _customerRepo.GetCustomerCountByCountry("Denmark");
        public int GetFinnishCustomerCount() => _customerRepo.GetCustomerCountByCountry("Finland");


        public int GetSwedishAccountCount() => _accountRepo.GetSwedishAccountCount();
        public int GetDanishAccountCount() => _accountRepo.GetDanishAccountCount();
        public int GetNorwegianAccountCount() => _accountRepo.GetNorwegianAccountCount();
        public int GetFinnishAccountCount() => _accountRepo.GetFinnishAccountCount();

        public decimal GetSwedishCapital() => _accountRepo.GetSwedishCapital();
        public decimal GetDanishCapital() => _accountRepo.GetDanishCapital();
        public decimal GetNorwegianCapital() => _accountRepo.GetNorwegianCapital();
        public decimal GetFinnishCapital() => _accountRepo.GetFinnishCapital();

        public int GetTotalAccounts() => _accountRepo.GetAccountCount();
    }
}
