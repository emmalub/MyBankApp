using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services.Repositories
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        List<Transaction> GetByAccountId(int accountId, int skip = 0, int take = 20);
    }
}
