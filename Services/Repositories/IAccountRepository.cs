using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;

namespace Services.Repositories
{
    public interface IAccountRepository
    {
        AccountDTO GetById(int accountId);
        void UpdateAccount(Account account);
    }
}
