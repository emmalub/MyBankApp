using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;

namespace Services.Services.Interfaces
{
    public interface IUserService
    {
        List<IdentityUser> GetAllUsers();
    }

}
