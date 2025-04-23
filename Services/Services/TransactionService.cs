using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using Services.Services.Interfaces;
using Services.Repositories.Interfaces;
using Services.Infrastructure.Paging;



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
                Date = t.Date,
            }).ToList();
        }

        public IQueryable<TransactionDTO> GetSortedTransactions(int accountId, string sortColumn, string sortOrder)
        {
            var query = _transactionRepo.GetByAccountId(accountId).AsQueryable();

            return query.Select(t => new TransactionDTO
            {
                Type = t.Type,
                Amount = t.Amount,
                Balance = t.Balance,
                Date = t.Date,
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


        public ResponseCode Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            if (fromAccountId == toAccountId)
            { 
                return ResponseCode.SameAccount;
            }

            if (amount < 100 || amount > 10000)
            {
                return ResponseCode.IncorrectAmount;
            }

            var frontAccountDto = _accountRepo.GetById(fromAccountId);
            var toAccountDto = _accountRepo.GetById(toAccountId);

            if (frontAccountDto == null || toAccountDto == null)
            {
                return ResponseCode.AccountNotFound;
            }

            if (frontAccountDto.Balance < amount)
            {
                return ResponseCode.BalanceTooLow;
            }
            var frontAccount = _mapper.Map<Account>(frontAccountDto);
            var toAccount = _mapper.Map<Account>(toAccountDto);

            frontAccount.Balance -= amount;
            toAccount.Balance += amount;

            _accountRepo.UpdateAccount(frontAccount);
            _accountRepo.UpdateAccount(toAccount);

            var transactionFrom = new Transaction
            {
                AccountId = fromAccountId,
                Amount = -amount,
                Balance = frontAccount.Balance - amount,
                Type = "Debit",
                Operation = "Transfer to account " + toAccountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            var transactionTo = new Transaction
            {
                AccountId = toAccountId,
                Amount = amount,
                Balance = toAccount.Balance + amount,
                Type = "Credit",
                Operation = "Transfer from account " + fromAccountId,
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            _transactionRepo.Add(transactionFrom);
            _transactionRepo.Add(transactionTo);
            return ResponseCode.OK;
        }


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

        public PagedResult<TransactionDTO> GetTransactionsByAccount(int accountNumber, int page, int pageSize)
        {
            var query = _transactionRepo.GetTransactionsByAccount(accountNumber);

            var paged = query.GetPaged(page, pageSize);

            return new PagedResult<TransactionDTO>
            {
                CurrentPage = paged.CurrentPage,
                PageCount = paged.PageCount,
                PageSize = paged.PageSize,
                RowCount = paged.RowCount,
                Results = _mapper.Map<List<TransactionDTO>>(paged.Results),
            };
        }

       
    }
}
