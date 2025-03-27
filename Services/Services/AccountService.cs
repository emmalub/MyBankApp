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
        public List<AccountDTO> GetAllAccounts()
        {
            return _repository.GetAllAccounts()
                .Include(a => a.Loans)
                .Select(a => new AccountDTO
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Created = a.Created,
                    LoansTotal = a.Loans.Sum(l => l.Amount) 
                })
                .ToList();
        }
        public IQueryable<AccountDTO> GetSortedAccounts(string sortColumn, string sortOrder, string q)
        {
            var query = _repository.GetAllAccounts()
       .Select(a => new AccountDTO
       {
           AccountId = a.AccountId,
           Balance = a.Balance,
           Created = a.Created,
           LoansTotal = a.Loans.Sum(l => l.Amount) // ✅ Summerar lånens belopp
       });

            return query;

            //var query = _repository.GetAllAccounts().AsQueryable();

            //if (!string.IsNullOrEmpty(q))
            //{
            //    query = query.Where(a =>
            //         a.AccountId.ToString().Contains(q) ||
            //         a.Balance.ToString().Contains(q));
            //}


            //// Sortering
            //query = sortColumn switch
            //{
            //    "Balance" => sortOrder == "desc" ? query.OrderByDescending(a => a.Balance) : query.OrderBy(a => a.Balance),
            //    _ => sortOrder == "desc" ? query.OrderByDescending(a => a.AccountId) : query.OrderBy(a => a.AccountId)
            //};

            //return query;
        }

        public AccountDTO GetAccountDetails(int accountId)
        {
            var account = _repository.GetAllAccounts()
                .Include(a => a.Transactions) // Ladda transaktioner
                .FirstOrDefault(a => a.AccountId == accountId);

            if (account == null)
                return null;

            return new AccountDTO
            {
                AccountId = account.AccountId,
                Balance = account.Balance,
                Created = account.Created,
                Loans = account.Loans.Select(l => new LoanDTO
                {
                    LoanId = l.LoanId,
                    Amount = l.Amount,
                    Status = l.Status,
                    Payments = l.Payments,
                }).ToList(),
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