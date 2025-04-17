using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.entity;

namespace CareerHub.dao
{
    public interface IJobBoardDao
    {
        int AddJobListing(JobListing job);
        int AddCompany(Company company);
        int AddApplicant(Applicant applicant);
        int AddJobApplication(JobApplication application);
        JobListing GetJobListingById(int id);
        Company GetCompanyById(int id);
        Applicant GetApplicantById(int id);
        List<JobListing> GetAllJobListings();
        List<Company> GetAllCompanies();
        List<Applicant> GetAllApplicants();
        List<JobApplication> GetApplicationsForJob(int jobId);
        List<JobListing> GetJobsBySalaryRange(decimal minSalary, decimal maxSalary);
    }
}
