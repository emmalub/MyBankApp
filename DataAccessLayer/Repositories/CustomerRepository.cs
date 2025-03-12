using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{

    public class CustomerRepository
    {
        private readonly BankAppDataContext _context;

        public CustomerRepository(BankAppDataContext context)
        {
            _context = context;
        }

        public int GetCustomerCount()
        {
            return _context.Customers.Count();
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
    }

}
