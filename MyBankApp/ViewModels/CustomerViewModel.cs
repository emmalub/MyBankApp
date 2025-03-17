namespace MyBankApp.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Name => $"{Givenname} {Surname}";
    //public List<CustomerViewModel> Customers { get; set; }

}
}