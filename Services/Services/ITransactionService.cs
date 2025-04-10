
using DataAccessLayer.DTOs;

namespace Services.Services
{
    public interface ITransactionService
    {
        ResponseCode Deposit(int accountId, decimal amount, string comment);
        ResponseCode Withdraw(int accountId, decimal amount);
        List<TransactionDTO> GetTransactions(int accountId, int skip = 0, int take = 20);
    }
}
