using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using MyBankApp.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum ResponseCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    CommentEmpty,
    AccountNotFound
}
namespace Services.Services
{
    public interface IAccountService
    {
        PagedResult<AccountDTO> GetSortedAccounts(string sortColumn, string sortOrder, string q, int page);
        public AccountDTO GetAccountDetails(int accountId);
        //public AccountDTO GetAccountById(int accountId);
    
    }
}
