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



        public List<Customer> GetSortedCustomers(string sortColumn, string sortOrder)
        {
            var query = _repository.GetAllCustomers().AsQueryable();

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.CustomerId);

            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query.OrderBy(c => c.Givenname).ThenBy(c => c.Surname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Givenname).ThenBy(c => c.Surname);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(c => c.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(c => c.City);
            
            return query.ToList();
        }

    }
}