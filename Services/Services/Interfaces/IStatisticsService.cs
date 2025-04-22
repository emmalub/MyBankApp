using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;


namespace Services.Services.Interfaces
{
    public interface IStatisticsService
    {
        // Kunder
        int GetSwedishCustomerCount();
        int GetNorwegianCustomerCount();
        int GetDanishCustomerCount();
        int GetFinnishCustomerCount();

        // Konton
        int GetSwedishAccountCount();
        int GetDanishAccountCount();
        int GetNorwegianAccountCount();
        int GetFinnishAccountCount();

        // Kapital
        decimal GetSwedishCapital();
        decimal GetDanishCapital();
        decimal GetNorwegianCapital();
        decimal GetFinnishCapital();

        int GetTotalCustomers();
        int GetTotalAccounts();
        decimal GetTotalCapital();
        int GetTotalUserCount();
    }
}
