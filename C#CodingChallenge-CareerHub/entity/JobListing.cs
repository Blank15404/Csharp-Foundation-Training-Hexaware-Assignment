using System;
using System.Collections.Generic;

namespace CareerHub.entity
{
    public class JobListing
    {
        public int JobID { get; set; }
        public int CompanyID { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime? Deadline { get; set; }

        public JobListing() { }

        public JobListing(int jobId, int companyId, string jobTitle, string jobDescription,
                         string jobLocation, decimal salary, string jobType, DateTime postedDate)
        {
            JobID = jobId;
            CompanyID = companyId;
            JobTitle = jobTitle;
            JobDescription = jobDescription;
            JobLocation = jobLocation;
            Salary = salary;
            JobType = jobType;
            PostedDate = postedDate;
        }
    }
}