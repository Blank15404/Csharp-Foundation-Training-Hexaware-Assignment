using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CareerHub.entity;
using CareerHub.exception;
using CareerHub.util;

namespace CareerHub.dao
{
    internal class JobBoardDaoImpl : IJobBoardDao
    {
        public int AddJobListing(JobListing job)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate, Deadline)
                           values (@JobID, @CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate, @Deadline)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@JobID", job.JobID);
                    cmd.Parameters.AddWithValue("@CompanyID", job.CompanyID);
                    cmd.Parameters.AddWithValue("@JobTitle", job.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", job.JobLocation);
                    cmd.Parameters.AddWithValue("@Salary", job.Salary);
                    cmd.Parameters.AddWithValue("@JobType", job.JobType);
                    cmd.Parameters.AddWithValue("@PostedDate", job.PostedDate);
                    cmd.Parameters.AddWithValue("@Deadline", job.Deadline ?? (object)DBNull.Value);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Job listing could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding job listing.", ex);
            }

            return rowsAffected;
        }

        public int AddCompany(Company company)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = "Insert into Companies (CompanyID, CompanyName, ComLocation) values (@CompanyID, @CompanyName, @Location)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    cmd.Parameters.AddWithValue("@Location", company.Location);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Company could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding company.", ex);
            }

            return rowsAffected;
        }

        public int AddApplicant(Applicant applicant)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Applicants (ApplicantID, FirstName, LastName, Email, Phone, AppResume)
                            values (@ApplicantID, @FirstName, @LastName, @Email, @Phone, @Resume)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ApplicantID", applicant.ApplicantID);
                    cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                    cmd.Parameters.AddWithValue("@Email", applicant.Email);
                    cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                    cmd.Parameters.AddWithValue("@Resume", applicant.Resume ?? (object)DBNull.Value);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Applicant could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding applicant.", ex);
            }

            return rowsAffected;
        }

        public int AddJobApplication(JobApplication application)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"Insert into Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
                            values (@ApplicationID, @JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
                    cmd.Parameters.AddWithValue("@JobID", application.JobID);
                    cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                    cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Job application could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding job application.", ex);
            }

            return rowsAffected;
        }

        public JobListing GetJobListingById(int id)
        {
            JobListing job = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = @"Select j.*, c.CompanyName from Jobs j join Companies c on j.CompanyID = c.CompanyID
                             where j.JobID = @JobID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@JobID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            job = new JobListing
                            {
                                JobID = (int)reader["JobID"],
                                CompanyID = (int)reader["CompanyID"],
                                JobTitle = (string)reader["JobTitle"],
                                JobDescription = (string)reader["JobDescription"],
                                JobLocation = (string)reader["JobLocation"],
                                Salary = (decimal)reader["Salary"],
                                JobType = (string)reader["JobType"],
                                PostedDate = (DateTime)reader["PostedDate"],
                                Deadline = reader["Deadline"] as DateTime?
                            };
                        }
                    }
                }

                if (job == null)
                {
                    throw new JobNotFoundException();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching job listing.", ex);
            }

            return job;
        }

        public Company GetCompanyById(int id)
        {
            Company company = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Companies where CompanyID = @CompanyID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CompanyID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            company = new Company
                            {
                                CompanyID = (int)reader["CompanyID"],
                                CompanyName = (string)reader["CompanyName"],
                                Location = (string)reader["ComLocation"]
                            };
                        }
                    }
                }

                if (company == null)
                {
                    throw new CompanyNotFoundException();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching company.", ex);
            }

            return company;
        }

        public Applicant GetApplicantById(int id)
        {
            Applicant applicant = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Applicants where ApplicantID = @ApplicantID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ApplicantID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicant = new Applicant
                            {
                                ApplicantID = (int)reader["ApplicantID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                Phone = (string)reader["Phone"],
                                Resume = reader["AppResume"] as string
                            };
                        }
                    }
                }

                if (applicant == null)
                {
                    throw new ApplicantNotFoundException();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching applicant.", ex);
            }

            return applicant;
        }

        public List<JobListing> GetAllJobListings()
        {
            List<JobListing> jobs = new List<JobListing>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = @"select j.*, c.CompanyName 
                            from Jobs j join Companies c ON j.CompanyID = c.CompanyID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobs.Add(new JobListing
                            {
                                JobID = (int)reader["JobID"],
                                CompanyID = (int)reader["CompanyID"],
                                JobTitle = (string)reader["JobTitle"],
                                JobDescription = (string)reader["JobDescription"],
                                JobLocation = (string)reader["JobLocation"],
                                Salary = (decimal)reader["Salary"],
                                JobType = (string)reader["JobType"],
                                PostedDate = (DateTime)reader["PostedDate"],
                                Deadline = reader["Deadline"] as DateTime?
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all job listings.", ex);
            }

            return jobs;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Companies";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            companies.Add(new Company
                            {
                                CompanyID = (int)reader["CompanyID"],
                                CompanyName = (string)reader["CompanyName"],
                                Location = (string)reader["ComLocation"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all companies.", ex);
            }

            return companies;
        }

        public List<Applicant> GetAllApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Applicants";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applicants.Add(new Applicant
                            {
                                ApplicantID = (int)reader["ApplicantID"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"],
                                Phone = (string)reader["Phone"],
                                Resume = reader["AppResume"] as string
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all applicants.", ex);
            }

            return applicants;
        }

        public List<JobApplication> GetApplicationsForJob(int jobId)
        {
            List<JobApplication> applications = new List<JobApplication>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Applications where JobID = @JobID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@JobID", jobId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applications.Add(new JobApplication
                            {
                                ApplicationID = (int)reader["ApplicationID"],
                                JobID = (int)reader["JobID"],
                                ApplicantID = (int)reader["ApplicantID"],
                                ApplicationDate = (DateTime)reader["ApplicationDate"],
                                CoverLetter = (string)reader["CoverLetter"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving applications for job.", ex);
            }

            return applications;
        }

        public List<JobListing> GetJobsBySalaryRange(decimal minSalary, decimal maxSalary)
        {
            List<JobListing> jobs = new List<JobListing>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = @"Select j.*, c.CompanyName from Jobs j join Companies c on j.CompanyID = c.CompanyID
                            where j.Salary between @MinSalary AND @MaxSalary";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@MinSalary", minSalary);
                    cmd.Parameters.AddWithValue("@MaxSalary", maxSalary);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            jobs.Add(new JobListing
                            {
                                JobID = (int)reader["JobID"],
                                CompanyID = (int)reader["CompanyID"],
                                JobTitle = (string)reader["JobTitle"],
                                JobDescription = (string)reader["JobDescription"],
                                JobLocation = (string)reader["JobLocation"],
                                Salary = (decimal)reader["Salary"],
                                JobType = (string)reader["JobType"],
                                PostedDate = (DateTime)reader["PostedDate"],
                                Deadline = reader["Deadline"] as DateTime?
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving jobs by salary range.", ex);
            }

            return jobs;
        }
    }
}