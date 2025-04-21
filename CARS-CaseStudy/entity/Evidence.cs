using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.entity
{
    public class Evidence
    {
        public int EvidenceID { get; set; }
        public string Description { get; set; }
        public string LocationFound { get; set; }
        public int IncidentID { get; set; }

        public Evidence() { }

        public Evidence(int evidenceID, string description, string locationFound, int incidentID)
        {
            EvidenceID = evidenceID;
            Description = description;
            LocationFound = locationFound;
            IncidentID = incidentID;
        }
    }
}
