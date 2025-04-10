using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CountrySummaryDTO
    {
        public string Country { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
