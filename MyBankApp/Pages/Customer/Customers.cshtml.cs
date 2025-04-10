using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBankApp.ViewModels;
using Services.Services.Interfaces;
using static MyBankApp.ViewModels.CustomerViewModel;


namespace MyBankApp.Pages.Customer
{
    [Authorize(Roles = "Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; } = 1;
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public int PageSize { get; set; } = 25;
        public string Q { get; set; }
        public int PageCount { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(string sortColumn = "Name", string sortOrder = "asc", int pageNo = 1, string q = "")
        {
            Q = q;

            SortColumn = sortColumn;
            SortOrder = sortOrder;
            
            if (pageNo == 0)
                pageNo = 1;

            CurrentPage = pageNo;

            var result = _customerService.GetSortedCustomers(sortColumn, sortOrder, q, pageNo);
           
            Customers = result.Results
            .Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                City = c.City,
                Country = c.Country,
            }).ToList();


            PageCount = result.PageCount;

            if (!Customers.Any())
            {
                ErrorMessage = "No customer found with the given search criteria.";
            }

            //var pagedResult = _customerService.GetSortedCustomers(SortColumn, SortOrder, q, pageNo, pageSize);


            //Customers = CustomerMapper.MapToViewModel(pagedResult.Results);
            //TotalPages = pagedResult.TotalPages;

        }
    }
}
