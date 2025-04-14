using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DTOs;
using AutoMapper;
using Services.Repositories.Interfaces;



namespace Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankAppDataContext _dbContext;
        private readonly IMapper _mapper;

        public AccountRepository(BankAppDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IQueryable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts
                .Include(a => a.Loans)
                .AsQueryable();
        }
      
        public AccountDTO GetById(int accountId)
        {
            var account = _dbContext.Accounts
                .Include(a => a.Loans)
                .First(a => a.AccountId == accountId);

            return _mapper.Map<AccountDTO>(account);
        }
        public void UpdateAccount(Account account)
        {
            var existingAccount = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);

            if (existingAccount != null)
            {
                existingAccount.Balance = account.Balance;
                _dbContext.SaveChanges();
            }
        }
        public void UpdateBalance(int accountId, decimal newBalance)
        {
            var account = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (account != null)
            {
                account.Balance = newBalance;
                _dbContext.SaveChanges();
            }
        }

        // methods for getting account counts and capital by country
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
                          .Where(a => a.Dispositions.Any(d => d.Customer.Country == "Sweden"))
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
