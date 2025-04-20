using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Services.Infrastructure.Paging;
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
namespace Services.Services.Interfaces
{
    public interface IAccountService
    {
        PagedResult<AccountDTO> GetSortedAccounts(string sortColumn, string sortOrder, string q, int page);
        public AccountDTO GetAccountDetails(int accountId);
        public void CreateAccount(Customer customer);
        //public void UpdateAccount(int accountId, AccountDTO accountDTO);
        //public void DeleteAccount(int accountId);
        //public void RestoreAccount(int accountId);



    }
}
