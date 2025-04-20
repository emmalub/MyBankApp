using DataAccessLayer.DTOs;
using System.ComponentModel.DataAnnotations;


namespace MyBankApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Set First Name.")]
        public string Givenname { get; set; }

        [Required(ErrorMessage = "Set Last Name.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Set City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Set Zipcode.")]
        public string Zipcode { get; set; }

        [Required(ErrorMessage = "Set Country.")]
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Set Streetaddress.")]
        public string Streetaddress { get; set; }
        public int Age { get; set; }

        [Required(ErrorMessage = "Set Gender.")]
        public string Gender { get; set; }
        public string NationalId { get; set; }
        public string Name { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
        public decimal TotalBalance { get; set; }

    
    }
}