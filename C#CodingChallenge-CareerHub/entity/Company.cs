using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.entity
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }

        public Company() { }

        public Company(int companyId, string companyName, string location)
        {
            CompanyID = companyId;
            CompanyName = companyName;
            Location = location;
        }
    }
}