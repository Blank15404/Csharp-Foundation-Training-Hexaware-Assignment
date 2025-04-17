using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.exception
{
    public class CareerException : Exception
    {
        public CareerException(string message) : base(message) { }
    }

    public class JobNotFoundException : CareerException
    {
        public JobNotFoundException() : base("Job not found") { }
    }

    public class CompanyNotFoundException : CareerException
    {
        public CompanyNotFoundException() : base("Company not found") { }
    }

    public class ApplicantNotFoundException : CareerException
    {
        public ApplicantNotFoundException() : base("Applicant not found") { }
    }
}
