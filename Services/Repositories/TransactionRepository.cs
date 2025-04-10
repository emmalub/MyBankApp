using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankAppDataContext _dbContext;

        public TransactionRepository(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public List<Transaction> GetByAccountId(int accountId, int skip = 0, int take = 20)
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
        public int CountByAccountId(int accountId)
        {
            return _dbContext.Transactions.Count(t => t.AccountId == accountId);
        }
    }

}
