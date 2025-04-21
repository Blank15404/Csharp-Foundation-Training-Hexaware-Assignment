using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.entity
{
    public class LawEnforcementAgency
    {
        public int AgencyID { get; set; }
        public string AgencyName { get; set; }
        public string Jurisdiction { get; set; }
        public string ContactInformation { get; set; }

        public LawEnforcementAgency() { }

        public LawEnforcementAgency(int agencyID, string agencyName, string jurisdiction, string contactInformation)
        {
            AgencyID = agencyID;
            AgencyName = agencyName;
            Jurisdiction = jurisdiction;
            ContactInformation = contactInformation;
        }
    }
}
