using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        //private readonly ICustomerService _customerSerice;
        private readonly CustomerRepository _repository;
        //private readonly BankAppDataContext _dbContext;

       
        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
            //_dbContext = dbContext;
        }



        public IQueryable<Customer> GetSortedCustomers(string sortColumn, string sortOrder, string q)
        {
            var query = _repository.GetAllCustomers().AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(c => c.Givenname.Contains(q) || c.Surname.Contains(q) || c.City.Contains(q) || c.Country.Contains(q));
            }

            switch (sortColumn)
            {
                case "Id":
                    query = (sortOrder == "asc") ? query.OrderBy(c => c.CustomerId) : query.OrderByDescending(c => c.CustomerId);
                    break;

                case "Name":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(c => c.Givenname).ThenBy(c => c.Surname) :
                        query.OrderByDescending(c => c.Givenname).ThenByDescending(c => c.Surname);
                    break;

                case "Country":
                    query = (sortOrder == "asc") ? query.OrderBy(c => c.Country) : query.OrderByDescending(c => c.Country);
                    break;

                case "City":
                    query = (sortOrder == "asc") ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);
                    break;

                default:
                    query = query.OrderBy(c => c.CustomerId); 
                    break;
            }

            return query;
        }

        public Customer GetCustomerDetails(int customerId)
        {
            return _repository.GetAllCustomers()
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefault(c => c.CustomerId == customerId);
        }
        public Customer GetCustomerWithDispositions(int id)
        {
            return _repository.GetAllCustomers()
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefault(c => c.CustomerId == id);
        }
    }
}