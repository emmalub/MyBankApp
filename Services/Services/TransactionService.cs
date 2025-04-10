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

        public TransactionService(IAccountRepository accountRepo, ITransactionRepository transactionRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
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
        public ResponseCode Withdraw(int accountId, decimal amount)
        {
            var accountDto = _accountRepo.GetById(accountId);

            if (accountDto == null)
                return ResponseCode.AccountNotFound;

            if (amount < 100 || amount > 10000)
                return ResponseCode.IncorrectAmount;

            if (accountDto.Balance < amount)
                return ResponseCode.BalanceTooLow;

            var newBalance = accountDto.Balance - amount;
            _accountRepo.UpdateBalance(accountId, newBalance);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = -amount,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Withdraw",
                Balance = newBalance 
            };

            _transactionRepo.Add(transaction);
            return ResponseCode.OK;
        }

        public ResponseCode Deposit(int accountId, decimal amount, string comment)
        {
            var accountDto = _accountRepo.GetById(accountId);

            if (accountDto == null)
                return ResponseCode.AccountNotFound;

            if (amount < 100 || amount > 10000)
                return ResponseCode.IncorrectAmount;

            if (string.IsNullOrWhiteSpace(comment))
                return ResponseCode.CommentEmpty;

            var newBalance = accountDto.Balance - amount;
            _accountRepo.UpdateBalance(accountId, newBalance);

            var transaction = new Transaction
            {
                AccountId = accountId,
                Amount = amount,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Type = "Deposit",
                Balance = newBalance // saldo efter insättning
            };

            _transactionRepo.Add(transaction);
            return ResponseCode.OK;
        }

    }
}
