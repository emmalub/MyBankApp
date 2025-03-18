using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Detta lager ansvarar för datalagring och hantering av CRUD-operationer.
    /// </summary>
    public class CustomerRepository
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

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
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
    }
}