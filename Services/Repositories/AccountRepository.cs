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

        public int GetDanishAccountCount()
        {
            return _dbContext.Accounts
                .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Denmark"))
                .Count();
        }

        public int GetFinnishAccountCount()
        {
            return _dbContext.Accounts
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Finland"))
                          .Count();
        }

        public int GetSwedishAccountCount()
        {
            return _dbContext.Accounts
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Swedish"))
                          .Count();
        }

        public int GetNorwegianAccountCount()
        {
            return _dbContext.Accounts
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Norway"))
                          .Count();
        }

        public decimal GetNorwegianCapital()
        {
            return _dbContext.Accounts
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Norway"))
                          .Sum(a => a.Balance);
        }

        public decimal GetDanishCapital()
        {
            return _dbContext.Accounts
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Denmark"))
                          .Sum(a => a.Balance);
        }

        public decimal GetFinnishCapital()
        {
            return _dbContext.Accounts
                                      .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Finland"))
                                      .Sum(a => a.Balance);
        }

        public decimal GetSwedishCapital()
        {
            return _dbContext.Accounts
                                      .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Sweden"))
                                      .Sum(a => a.Balance);
        }
    }
}
