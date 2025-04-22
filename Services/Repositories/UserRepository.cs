using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Services.Repositories.Interfaces;



namespace Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BankAppDataContext _context;
    }
}
