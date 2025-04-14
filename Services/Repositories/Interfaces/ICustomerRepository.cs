using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{

    public interface ICustomerRepository
    {
        int GetCustomerCount();
        IQueryable<Customer> GetAllCustomers();
        int GetCustomerCountByCountry(string country);
        Customer GetById(int customerId);
        public void Add(Customer customer);
        public void Update(Customer customer);
        public void Delete(int customerId);
    }
}
