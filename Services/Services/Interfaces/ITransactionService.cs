
using DataAccessLayer.DTOs;

namespace Services.Services.Interfaces
{
    public interface ITransactionService
    {
        ResponseCode Deposit(int accountId, decimal amount);
        ResponseCode Withdraw(int accountId, decimal amount);
        List<TransactionDTO> GetTransactions(int accountId, int skip = 0, int take = 20);
        IQueryable<TransactionDTO> GetSortedTransactions(int accountId, string sortColumn, string sortOrder);
        List<TransactionDTO> PaginateTransactions(IQueryable<TransactionDTO> transactions, int pageNo);
    }
}
