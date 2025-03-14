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
            var query = _repository.GetAllCustomers();

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.CustomerId);

            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Givenname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Givenname);

            if (sortColumn == "Surname")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Surname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Surname);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.City);
            
            return query.ToList();
        }

    }
}