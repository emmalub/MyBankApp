using DataAccessLayer.Models;
using Services.Repositories.Interfaces;



namespace Services.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly BankAppDataContext _dbContext;

        public StatisticsRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetCustomerCountByCountry(string country)
        {
            return _dbContext.Customers.Count(c => c.Country == country);
        }

        public int GetTotalCustomerCount()
        {
            return _dbContext.Customers.Count();
        }
        public int GetTotalUserCount()
        {
            return _dbContext.Users.Count();
        }
    }
}
