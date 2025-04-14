using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyBankApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;

namespace Services.Services.Interfaces
{
    public interface ICustomerService
    {
        PagedResult<CustomerDTO> GetSortedCustomers(string sortColumn, string sortOrder, string q, int page);
        Customer GetCustomerDetails(int customerId);
        Customer GetCustomerWithDispositions(int id);

        CustomerDTO GetCustomerById(int customerId);
        void CreateCustomer(CustomerDTO customer);
        void UpdateCustomer(int customerId, CustomerDTO customer);
        void DeleteCustomer(int customerId);

    }
}
