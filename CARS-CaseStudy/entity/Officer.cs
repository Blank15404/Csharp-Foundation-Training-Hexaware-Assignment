using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.entity
{
    public class Officer
    {
        public int OfficerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BadgeNumber { get; set; }
        public string Rank { get; set; }
        public string ContactInformation { get; set; }
        public int AgencyID { get; set; }

        public Officer() { }

        public Officer(int officerID, string firstName, string lastName, string badgeNumber,
                       string rank, string contactInformation, int agencyID)
        {
            OfficerID = officerID;
            FirstName = firstName;
            LastName = lastName;
            BadgeNumber = badgeNumber;
            Rank = rank;
            ContactInformation = contactInformation;
            AgencyID = agencyID;
        }
    }
}