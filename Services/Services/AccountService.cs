using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure.Paging;
using Services.Repositories;
using AutoMapper;
using Services.Services.Interfaces;
using Services.Repositories.Interfaces;
using DataAccessLayer.Models;


namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _accountRepo = repository;
            _mapper = mapper;
        }
        public List<AccountDTO> GetAllAccounts()
        {
            return _accountRepo.GetAllAccounts()
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


        public PagedResult<AccountDTO> GetSortedAccounts(string sortColumn, string sortOrder, string q, int page)
        {
            var query = _accountRepo.GetAllAccounts()
                .Select(a => new AccountDTO
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Created = a.Created,
                    LoansTotal = a.Loans.Sum(l => l.Amount)
                });

            if (!string.IsNullOrEmpty(q))
            {
                int.TryParse(q, out int id);

                query = query.Where(a =>
                a.AccountId == id);
            }

            switch (sortColumn)
            {
                case "AccountId":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(a => a.AccountId) :
                        query.OrderByDescending(c => c.AccountId);
                    break;

                case "Balance":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(a => a.Balance) :
                        query.OrderByDescending(c => c.Balance);
                    break;

                case "Created":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(a => a.Created) :
                        query.OrderByDescending(c => c.Created);
                    break;

                case "LoansTotal":
                    query = (sortOrder == "asc") ?
                        query.OrderBy(a => a.LoansTotal) :
                        query.OrderByDescending(a => a.LoansTotal);
                    break;

                default:
                    query = query.OrderBy(c => c.AccountId);
                    break;
            }

            return query.GetPaged(page, 50);
        }

        public AccountDTO GetAccountDetails(int accountId)
        {
            var account = _accountRepo.GetAllAccounts()
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
                        Date = t.Date,
                        Amount = t.Amount,
                        Type = t.Type,
                        Balance = t.Balance,
                    }).ToList()
            };
        }

        public void CreateAccount(Customer customer)
        {
            var account = new Account
            {
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = 0,
                Frequency = "Monthly",
            };
            
            var disposition = new Disposition
            {
                Customer = customer,
                Account = account,
                Type = "OWNER",
            };

            account.Dispositions.Add(disposition);

            _accountRepo.Add(account);
        }
    }
}