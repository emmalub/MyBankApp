using DataAccessLayer.DTOs;

namespace MyBankApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Email { get; set; }
        public string Streetaddress { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
        public decimal TotalBalance { get; set; }

    
    }
}