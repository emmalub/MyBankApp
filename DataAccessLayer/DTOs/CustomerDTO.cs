using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string NationalId{ get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Telephonenumber { get; set; }
        public string Telephonecountrycode { get; set; }
        public string Emailaddress { get; set; }
        public DateOnly? Birthday { get; set; }
        public string Gender { get; set; }
        public ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();
        public string Name => $"{Givenname} {Surname}";


    }
}
