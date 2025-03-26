using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IAccountService
    {
        IQueryable<Account> GetSortedAccounts(string sortColumn, string sortOrder, string q);

        public AccountDetailsDTO GetAccountDetails(int accountId);
    }
}
