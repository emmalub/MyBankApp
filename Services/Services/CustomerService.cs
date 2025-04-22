using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using AutoMapper;
using Services.Services.Interfaces;
using Services.Repositories.Interfaces;


namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IAccountService _accountService;

        public CustomerService(IMapper mapper, ICustomerRepository repository, IAccountRepository accountRepo, IAccountService accountService)
        {
            _mapper = mapper;
            _customerRepo = repository;
            _accountRepo = accountRepo;
            _accountService = accountService;
        }

        public List<CustomerDTO> GetCustomers()
        {
            var query = _customerRepo.GetAllCustomers()
                 //.Where(c = c.CustomerId == id && c.IsActive)
                 .Select(c => new CustomerDTO
                 {
                     Id = c.CustomerId,
                     NationalId = c.NationalId,
                     Givenname = c.Givenname,
                     Surname = c.Surname,
                     Streetaddress = c.Streetaddress,
                     City = c.City,
                     Country = c.Country,
                     CountryCode = c.CountryCode,
                     Birthday = c.Birthday,
                     Telephonenumber = c.Telephonenumber,
                     Telephonecountrycode = c.Telephonecountrycode,
                     Emailaddress = c.Emailaddress,
                     Gender = c.Gender,
                 });
            return query.ToList();
        }

        public CustomerDTO GetCustomerById(int customerId)
        {
            var customer = _customerRepo.GetById(customerId);

            if (customer == null)
            {
                return null;
            }
            var customerDTO = _mapper.Map<CustomerDTO>(customer);

            return new CustomerDTO
            {
                Id = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                City = customer.City,
                Country = customer.Country,
            };
        }
        public void CreateCustomer(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Givenname = customerDTO.Givenname,
                Surname = customerDTO.Surname,
                City = customerDTO.City,
                Country = customerDTO.Country,
                CountryCode = customerDTO.CountryCode,
                Telephonecountrycode = customerDTO.Telephonecountrycode,
                Telephonenumber = customerDTO.Telephonenumber,
                Emailaddress = customerDTO.Emailaddress,
                Streetaddress = customerDTO.Streetaddress,
                Zipcode = customerDTO.Zipcode,
                NationalId = customerDTO.NationalId,
                Gender = customerDTO.Gender,
                Birthday = customerDTO.Birthday,
                IsActive = true
            };

            _customerRepo.Add(customer);

            _accountService.CreateAccount(customer);
            _customerRepo.SaveChanges();
        }

        //SKAPADE AV AI JUST NU
        public void UpdateCustomer(int customerId, CustomerDTO customerDTO)
        {
            var customer = _customerRepo.GetById(customerDTO.Id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            customer.Givenname = customerDTO.Givenname;
            customer.Surname = customerDTO.Surname;
            customer.City = customerDTO.City;
            customer.Country = customerDTO.Country;
            _customerRepo.Update(customer);
        }
        // SKAPAT AV AI HIT
        public void DeleteCustomer(int customerId)
        {
            _customerRepo.Delete(customerId);
        }
        public void RestoreCustomer(int customerId)
        {
            var customer = _customerRepo.GetById(customerId);

            if (customer != null)
            {
                customer.IsActive = true;
                _customerRepo.Update(customer);
            }
        }
        public PagedResult<CustomerDTO> GetSortedCustomers(string sortColumn, string sortOrder, string q, int page)
        {
            var query = _customerRepo.GetAllCustomers()
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

            return query.GetPaged(page, 50);

        }

        public Customer GetCustomerDetails(int customerId)
        {
            return _customerRepo.GetAllCustomers()
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefault(c => c.CustomerId == customerId);
        }
        public Customer GetCustomerWithDispositions(int id)
        {
            return _customerRepo.GetAllCustomers()
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefault(c => c.CustomerId == id);
        }

    }
}