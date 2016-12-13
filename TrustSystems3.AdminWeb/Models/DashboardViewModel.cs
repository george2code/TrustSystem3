using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrustSystems3.AdminWeb.Models
{
    public class DashboardViewModel
    {
        public int ReviewsCount { get; set; }
        public int CompaniesCount { get; set; }
        public int UsersCount { get; set; }
        public IEnumerable<Logger> Logs { get; set; }
    }
}