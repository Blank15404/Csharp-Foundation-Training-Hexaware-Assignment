using System;
using System.Collections.Generic;
using CareerHub.entity;

namespace CareerHub
{
    class UserInterface
    {
        public int GetMainMenuChoice()
        {
            Console.WriteLine("\nCareerHub Job Board");
            Console.WriteLine("1. Company Menu");
            Console.WriteLine("2. Applicant Menu");
            Console.WriteLine("3. Job Listing Menu");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetCompanyMenuChoice()
        {
            Console.WriteLine("\nCompany Menu");
            Console.WriteLine("1. Add Company");
            Console.WriteLine("2. View All Companies");
            Console.WriteLine("3. Post Job");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Company GetCompanyDetails()
        {
            Console.Write("Enter Company ID: ");
            int companyId = int.Parse(Console.ReadLine());
            Console.Write("Enter Company Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Location: ");
            string location = Console.ReadLine();

            return new Company(companyId, name, location);
        }

        public void DisplayCompanies(List<Company> companies)
        {
            foreach (var company in companies)
            {
                Console.WriteLine($"\nCompany ID: {company.CompanyID}");
                Console.WriteLine($"Name: {company.CompanyName}");
                Console.WriteLine($"Location: {company.Location}");
            }
        }

        public JobListing GetJobDetails()
        {
            Console.Write("Enter Job ID: ");
            int jobId = int.Parse(Console.ReadLine());
            Console.Write("Enter Company ID: ");
            int companyId = int.Parse(Console.ReadLine());
            Console.Write("Enter Job Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Job Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Job Location: ");
            string location = Console.ReadLine();
            Console.Write("Enter Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Job Type: ");
            string jobType = Console.ReadLine();
            Console.Write("Enter Deadline (yyyy-mm-dd): ");
            DateTime? deadline = DateTime.TryParse(Console.ReadLine(), out var dt) ? dt : (DateTime?)null;

            return new JobListing(jobId, companyId, title, description, location, salary, jobType, DateTime.Now)
            {
                Deadline = deadline
            };
        }

        public int GetApplicantMenuChoice()
        {
            Console.WriteLine("\nApplicant Menu");
            Console.WriteLine("1. Create Profile");
            Console.WriteLine("2. Apply for Job");
            Console.WriteLine("3. View All Applicants");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Applicant GetApplicantDetails()
        {
            Console.Write("Enter Applicant ID: ");
            int applicantId = int.Parse(Console.ReadLine());
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Resume: ");
            string resume = Console.ReadLine();

            return new Applicant(applicantId, firstName, lastName, email, phone, resume);
        }

        public JobApplication GetApplicationDetails()
        {
            Console.Write("Enter Application ID: ");
            int appId = int.Parse(Console.ReadLine());
            Console.Write("Enter Applicant ID: ");
            int applicantId = int.Parse(Console.ReadLine());
            Console.Write("Enter Job ID: ");
            int jobId = int.Parse(Console.ReadLine());
            Console.Write("Enter Cover Letter: ");
            string coverLetter = Console.ReadLine();

            return new JobApplication(appId, jobId, applicantId, DateTime.Now, coverLetter);
        }

        public void DisplayApplicants(List<Applicant> applicants)
        {
            foreach (var applicant in applicants)
            {
                Console.WriteLine($"\nApplicant ID: {applicant.ApplicantID}");
                Console.WriteLine($"Name: {applicant.FirstName} {applicant.LastName}");
                Console.WriteLine($"Email: {applicant.Email}");
                Console.WriteLine($"Phone: {applicant.Phone}");
            }
        }

        public int GetJobListingMenuChoice()
        {
            Console.WriteLine("\nJob Listing Menu");
            Console.WriteLine("1. View All Jobs");
            Console.WriteLine("2. View Jobs by Salary Range");
            Console.WriteLine("3. View Applicants for Job");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayJobs(List<JobListing> jobs)
        {
            foreach (var job in jobs)
            {
                Console.WriteLine($"\nJob ID: {job.JobID}");
                Console.WriteLine($"Title: {job.JobTitle}");
                Console.WriteLine($"Company ID: {job.CompanyID}");
                Console.WriteLine($"Location: {job.JobLocation}");
                Console.WriteLine($"Salary: {job.Salary}");
                Console.WriteLine($"Posted: {job.PostedDate.ToShortDateString()}");
                if (job.Deadline.HasValue)
                    Console.WriteLine($"Deadline: {job.Deadline.Value.ToShortDateString()}");
            }
        }

        public (decimal, decimal) GetSalaryRange()
        {
            Console.Write("Enter Minimum Salary: ");
            decimal min = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Maximum Salary: ");
            decimal max = decimal.Parse(Console.ReadLine());
            return (min, max);
        }

        public int GetJobIdForApplications()
        {
            Console.Write("Enter Job ID: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayApplications(List<JobApplication> applications)
        {
            foreach (var app in applications)
            {
                Console.WriteLine($"\nApplication ID: {app.ApplicationID}");
                Console.WriteLine($"Applicant ID: {app.ApplicantID}");
                Console.WriteLine($"Date: {app.ApplicationDate.ToShortDateString()}");
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string error)
        {
            Console.WriteLine($"Error: {error}");
        }
    }
}