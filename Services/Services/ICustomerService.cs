using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Services.Services
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetSortedCustomers(string sortColumn, string sortOrder, string q);
    }
}
