using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;


namespace Services.Repositories
{
    /// <summary>
    /// Detta lager ansvarar för datalagring och hantering av CRUD-operationer.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        public int GetCustomerCount()
        {
            return _dbContext.Customers.Count();
        }
        public int GetCustomerCountByCountry(string country)
        {
            return _dbContext.Customers
                .Where(c => c.Country == country)
                .Count();
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.AsQueryable();
        }


        public int GetSwedishCustomerCount()
        {
            return _dbContext.Customers
                .Where(c => c.Country == "Sweden")  
                .Count(); 
        }
        public int GetNorwegianCustomerCount()
        {
            return _dbContext.Customers
                .Where(c => c.Country == "Norway")
                .Count();
        }
        public int GetDanishCustomerCount()
        {
            return _dbContext.Customers
                .Where(c => c.Country == "Denmark")
                .Count();
        }
        public int GetFinnishCustomerCount()
        {
            return _dbContext.Customers
                .Where(c => c.Country == "Finland")
                .Count();
        }
    }
}