using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _repository;

        public AccountService(AccountRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Account> GetSortedAccounts(string sortColumn, string sortOrder, string q)
        {
            var query = _repository.GetAllAccounts().AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(a =>
                     a.AccountId.ToString().Contains(q) ||
                     a.Balance.ToString().Contains(q));
            }


            // Sortering
            query = sortColumn switch
            {
                "Balance" => sortOrder == "desc" ? query.OrderByDescending(a => a.Balance) : query.OrderBy(a => a.Balance),
                _ => sortOrder == "desc" ? query.OrderByDescending(a => a.AccountId) : query.OrderBy(a => a.AccountId)
            };

            return query;
        }

        public AccountDetailsDTO GetAccountDetails(int accountId)
        {
            var account = _repository.GetAllAccounts()
                .Include(a => a.Transactions) // Ladda transaktioner
                .FirstOrDefault(a => a.AccountId == accountId);

            if (account == null)
                return null;

            return new AccountDetailsDTO
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Transactions = account.Transactions
                    .Select(t => new TransactionDTO
                    {
                        Date = t.Date.ToDateTime(new TimeOnly(0,0)),
                        Amount = t.Amount,
                        Type = t.Type
                    }).ToList()
            };
        }
    }

}