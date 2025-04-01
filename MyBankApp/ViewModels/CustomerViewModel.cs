using DataAccessLayer.DTOs;

namespace MyBankApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string SocialSecurity { get; set; }
        public string Name { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
        public decimal TotalBalance { get; set; }

        // GÖR OM TILL AUTOMAPPER SEN
        public static class CustomerMapper
        {
            public static List<CustomerViewModel> MapToViewModel(List<CustomerDTO> customerDTOs)
            {
                return customerDTOs.Select(customer => new CustomerViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    City = customer.City,
                    Country = customer.Country
                }).ToList();
            }
        }
    }
}