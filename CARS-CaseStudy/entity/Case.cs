using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.entity
{
    public class Case
    {
        public int CaseID { get; set; }
        public string CaseDescription { get; set; }
        public List<Incident> Incidents { get; set; } = new List<Incident>();
    }
}
