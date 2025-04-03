using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using MyBankApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repository;

       
        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public PagedResult<CustomerDTO> GetSortedCustomers(string sortColumn, string sortOrder, string q, int page)
        {
            var query = _repository.GetAllCustomers()
                .Select(c => new CustomerDTO
                {
                    Id = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Country = c.Country,
                    City = c.City
                });

            if (!string.IsNullOrEmpty(q))
            {
                int.TryParse(q, out int id);

                query = query.Where(c =>
                c.Id == id ||
                c.Givenname.Contains(q) || 
                c.Surname.Contains(q) || 
                c.City.Contains(q) || 
                c.Country.Contains(q));
            }

            switch (sortColumn)
            {
                case "Id":
                    query = (sortOrder == "asc") ? 
                        query.OrderBy(c => c.Id) : 
                        query.OrderByDescending(c => c.Id);
                    break;

                case "Name":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(c => c.Givenname)
                        .ThenBy(c => c.Surname) :
                        query.OrderByDescending(c => c.Givenname)
                        .ThenByDescending(c => c.Surname);
                    break;

                case "Country":
                    query = (sortOrder == "asc") ? 
                        query.OrderBy(c => c.Country) : 
                        query.OrderByDescending(c => c.Country);
                    break;

                case "City":
                    query = (sortOrder == "asc") ? 
                        query.OrderBy(c => c.City) : 
                        query.OrderByDescending(c => c.City);
                    break;

                default:
                    query = query.OrderBy(c => c.Id); 
                    break;
            }
           
            return query.GetPaged(page, 25);

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