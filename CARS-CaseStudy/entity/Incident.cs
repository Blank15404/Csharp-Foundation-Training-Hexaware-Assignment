using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.entity
{
    public class Incident
    {
        public int IncidentID { get; set; }
        public string IncidentType { get; set; }
        public DateTime IncidentDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int VictimID { get; set; }
        public int SuspectID { get; set; }

        public Incident() { }

        public Incident(int incidentID, string incidentType, DateTime incidentDate, string location,
                       string description, string status, int victimID, int suspectID)
        {
            IncidentID = incidentID;
            IncidentType = incidentType;
            IncidentDate = incidentDate;
            Location = location;
            Description = description;
            Status = status;
            VictimID = victimID;
            SuspectID = suspectID;
        }
    }
}