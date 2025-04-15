using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;

namespace Services.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        AccountDTO GetById(int accountId);
        void UpdateAccount(Account account);
        void UpdateBalance(int accountId, decimal newBalance);
        void Add(Account account);
        void SaveChanges();
        IQueryable<Account> GetAllAccounts();

        int GetAccountCount();
        int GetSwedishAccountCount();
        int GetNorwegianAccountCount();
        int GetDanishAccountCount();
        int GetFinnishAccountCount();

        decimal GetSwedishCapital();
        decimal GetNorwegianCapital();
        decimal GetDanishCapital();
        decimal GetFinnishCapital();
    }
}
