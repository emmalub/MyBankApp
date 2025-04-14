using AutoMapper;
using DataAccessLayer.Models;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;
using Services.Services.Interfaces;
using Services.Repositories.Interfaces;



namespace Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IMapper _mapper;

        public TransactionService(IAccountRepository accountRepo, ITransactionRepository transactionRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }
        public List<TransactionDTO> GetTransactions(int accountId, int skip = 0, int take = 20)
        {
            var transactions = _transactionRepo.GetByAccountId(accountId, skip, take);

            return transactions.Select(t => new TransactionDTO
            {
                Type = t.Type,
                Amount = t.Amount,
                Balance = t.Balance,
                Date = t.Date.ToDateTime(TimeOnly.MinValue)
            }).ToList();
        }

        // NYTT 14/4
        public IQueryable<TransactionDTO> GetSortedTransactions(int accountId, string sortColumn, string sortOrder)
        {
            var query = _transactionRepo.GetByAccountId(accountId).AsQueryable(); 
            
            
            switch (sortColumn)
            {
                case "Date":
                    query = sortOrder == "asc" ? query.OrderBy(t => t.Date.ToDateTime(TimeOnly.MinValue)) : query.OrderByDescending(t => t.Date.ToDateTime(TimeOnly.MinValue));
                    break;
                case "Amount":
                    query = sortOrder == "asc" ? query.OrderBy(t => t.Amount) : query.OrderByDescending(t => t.Amount);
                    break;
                case "Type":
                    query = sortOrder == "asc" ? query.OrderBy(t => t.Type) : query.OrderByDescending(t => t.Type);
                    break;
                case "Balance":
                    query = sortOrder == "asc" ? query.OrderBy(t => t.Balance) : query.OrderByDescending(t => t.Balance);
                    break;
                default:
                    query = query.OrderBy(t => t.Date.ToDateTime(TimeOnly.MinValue));
                    break;
            }
            return query.Select(t => new TransactionDTO
            {
                Type = t.Type,
                Amount = t.Amount,
                Balance = t.Balance,
                Date = t.Date.ToDateTime(TimeOnly.MinValue)
            });
        }
        public List<TransactionDTO> PaginateTransactions(IQueryable<TransactionDTO> transactions, int pageNo)
        {
            const int PageSize = 20;

            var transactionsToPaginate = transactions
                .Skip((pageNo - 1) * PageSize)
                .Take(PageSize)
                .ToList();


            return transactionsToPaginate;
        }


        // SLUT NYTT 14/4



        public ResponseCode Withdraw(int accountId, decimal amount)
        {
            var accountDto = _accountRepo.GetById(accountId);

            if (accountDto == null)
                return ResponseCode.AccountNotFound;
            
            if (accountDto.Balance < amount)
                return ResponseCode.BalanceTooLow;

            if (amount < 100 || amount > 10000)
                return ResponseCode.IncorrectAmount;

            var account = _mapper.Map<Account>(accountDto);

            account.Balance -= amount;
            _accountRepo.UpdateAccount(account);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = -amount,
                Balance = account.Balance - amount,
                Type = "Debit",
                Operation = "Debit in cash",
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
            };


            _transactionRepo.Add(transaction);
            return ResponseCode.OK;
        }

        public ResponseCode Deposit(int accountId, decimal amount)
        {
            var accountDto = _accountRepo.GetById(accountId);

            if (accountDto == null)
                return ResponseCode.AccountNotFound;

            if (amount < 100 || amount > 10000)
                return ResponseCode.IncorrectAmount;
           
            var account = _mapper.Map<Account>(accountDto);

            account.Balance += amount;
            _accountRepo.UpdateAccount(account);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                Balance = account.Balance + amount,
                Type = "Credit",
                Operation = "Credit in cash",
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
            };


            _transactionRepo.Add(transaction);
            return ResponseCode.OK;
        }

    }
}
