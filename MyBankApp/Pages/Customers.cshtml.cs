using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MyBankApp.Pages
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly BankAppDataContext _dbContext;

        public CustomersModel(BankAppDataContext context)
        {
            _dbContext = context;
        }
        public List<CustomerViewModel> Customers { get; set; }

        public void OnGet(string sortColumn, string sortOrder)
        {
            var query = _dbContext.Customers.Select(s => new CustomerViewModel
            {
                Id = s.CustomerId,
                Name = s.Givenname,
                City = s.City,
                Country = s.Country
            });

            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Name);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Name);

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

            Customers = query.ToList();
        }
    }
}
