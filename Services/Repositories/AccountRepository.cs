using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace Services.Repositories
{
    public class AccountRepository
    {
        private readonly BankAppDataContext _dbContext;

        public AccountRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts
                .Include(a => a.Loans)
                .AsQueryable();
        }
            
        public Account GetAccountById(int accountId)
        {
            return _dbContext.Accounts
                .Include(a => a.Loans)
                .First(a => a.AccountId == accountId);
        }
        public int GetAccountCount()
        {
            return _dbContext.Accounts.Count();
        }
    }
}
