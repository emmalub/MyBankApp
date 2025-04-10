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

            CreateMap<AccountDTO, Account>()
           .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
           .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
           .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
           .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans));
        }
    }
}
