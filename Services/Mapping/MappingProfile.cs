using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using AutoMapper;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Account, AccountDTO>();
            CreateMap<Transaction, TransactionDTO>();
        }
    }
}
