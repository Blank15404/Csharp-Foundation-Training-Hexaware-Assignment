using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.exception
{
    public class OfficerNotFoundException : Exception
    {
        public OfficerNotFoundException() : base("Officer not found in database") { }
        public OfficerNotFoundException(string message) : base(message) { }
    }
}