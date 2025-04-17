using System;
using System.Collections.Generic;
using CareerHub.dao;
using CareerHub.entity;
using CareerHub.exception;

namespace CareerHub
{
    class Program
    {
        private static IJobBoardDao jobBoardDao = new JobBoardDaoImpl();
        private static UserInterface ui = new UserInterface();

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    int choice = ui.GetMainMenuChoice();

                    switch (choice)
                    {
                        case 1:
                            CompanyMenu();
                            break;
                        case 2:
                            ApplicantMenu();
                            break;
                        case 3:
                            JobListingMenu();
                            break;
                        case 4:
                            return;
                        default:
                            ui.ShowMessage("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void CompanyMenu()
        {
            while (true)
            {
                int choice = ui.GetCompanyMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddCompany();
                        break;
                    case 2:
                        ViewAllCompanies();
                        break;
                    case 3:
                        PostJob();
                        break;
                    case 4:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddCompany()
        {
            try
            {
                Company company = ui.GetCompanyDetails();
                int result = jobBoardDao.AddCompany(company);
                ui.ShowMessage("Company added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllCompanies()
        {
            try
            {
                var companies = jobBoardDao.GetAllCompanies();
                ui.DisplayCompanies(companies);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void PostJob()
        {
            try
            {
                JobListing job = ui.GetJobDetails();
                int result = jobBoardDao.AddJobListing(job);
                ui.ShowMessage("Job posted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ApplicantMenu()
        {
            while (true)
            {
                int choice = ui.GetApplicantMenuChoice();

                switch (choice)
                {
                    case 1:
                        CreateProfile();
                        break;
                    case 2:
                        ApplyForJob();
                        break;
                    case 3:
                        ViewAllApplicants();
                        break;
                    case 4:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CreateProfile()
        {
            try
            {
                Applicant applicant = ui.GetApplicantDetails();
                int result = jobBoardDao.AddApplicant(applicant);
                ui.ShowMessage("Profile created successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ApplyForJob()
        {
            try
            {
                JobApplication application = ui.GetApplicationDetails();
                int result = jobBoardDao.AddJobApplication(application);
                ui.ShowMessage("Application submitted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllApplicants()
        {
            try
            {
                var applicants = jobBoardDao.GetAllApplicants();
                ui.DisplayApplicants(applicants);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void JobListingMenu()
        {
            while (true)
            {
                int choice = ui.GetJobListingMenuChoice();

                switch (choice)
                {
                    case 1:
                        ViewAllJobs();
                        break;
                    case 2:
                        ViewJobsBySalaryRange();
                        break;
                    case 3:
                        ViewApplicantsForJob();
                        break;
                    case 4:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ViewAllJobs()
        {
            try
            {
                var jobs = jobBoardDao.GetAllJobListings();
                ui.DisplayJobs(jobs);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewJobsBySalaryRange()
        {
            try
            {
                var (min, max) = ui.GetSalaryRange();
                var jobs = jobBoardDao.GetJobsBySalaryRange(min, max);
                ui.DisplayJobs(jobs);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewApplicantsForJob()
        {
            try
            {
                int jobId = ui.GetJobIdForApplications();
                var applications = jobBoardDao.GetApplicationsForJob(jobId);
                ui.DisplayApplications(applications);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }
    }
}
