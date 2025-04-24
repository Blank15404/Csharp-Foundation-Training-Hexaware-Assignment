using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CARS_CaseStudy.util;
using CARS_CaseStudy.exception;
using CARS_CaseStudy.entity;
using System.Net;

namespace CARS_CaseStudy.dao
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        public bool CreateIncident(Incident incident)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string checkQuery = "SELECT COUNT(*) FROM Incidents WHERE IncidentID = @IncidentID";
            string insertQuery = @"INSERT INTO Incidents 
                        (IncidentID, IncidentType, IncidentDate, IncidentLocation, 
                         IncidentDescription, IncidentStatus, VictimID, SuspectID) 
                        VALUES 
                        (@IncidentID, @IncidentType, @IncidentDate, @Location, 
                         @Description, @Status, @VictimID, @SuspectID)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        throw new DuplicateIdException($"Incident with ID {incident.IncidentID} already exists");
                    }

                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);
                    cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                    cmd.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
                    cmd.Parameters.AddWithValue("@Location", incident.Location);
                    cmd.Parameters.AddWithValue("@Description", incident.Description);
                    cmd.Parameters.AddWithValue("@Status", incident.Status);
                    cmd.Parameters.AddWithValue("@VictimID", incident.VictimID);
                    cmd.Parameters.AddWithValue("@SuspectID", incident.SuspectID);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected > 0;
            }
            catch (DuplicateIdException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating incident: {ex.Message}");
                return false;
            }
        }

        public bool UpdateIncidentStatus(string status, int incidentId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string checkQuery = "SELECT COUNT(*) FROM Incidents WHERE IncidentID = @IncidentID";
            string updateQuery = "UPDATE Incidents SET IncidentStatus = @Status WHERE IncidentID = @IncidentID";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@IncidentID", incidentId);
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {incidentId} not found.");
                    }

                    cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@IncidentID", incidentId);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                return rowsAffected > 0;
            }
            catch (IncidentNumberNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating incident status: {ex.Message}");
                return false;
            }
        }

        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be after end date");
            }

            List<Incident> incidents = new List<Incident>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "SELECT * FROM Incidents WHERE IncidentDate BETWEEN @StartDate AND @EndDate";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incidents.Add(new Incident(
                                (int)reader["IncidentID"],
                                reader["IncidentType"].ToString(),
                                (DateTime)reader["IncidentDate"],
                                reader["IncidentLocation"].ToString(),
                                reader["IncidentDescription"].ToString(),
                                reader["IncidentStatus"].ToString(),
                                (int)reader["VictimID"],
                                (int)reader["SuspectID"]
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting incidents in date range: {ex.Message}");
            }

            return incidents;
        }

        public List<Incident> SearchIncidents(string criteria)
        {
            List<Incident> incidents = new List<Incident>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = @"SELECT * FROM Incidents 
                            WHERE IncidentType LIKE @Criteria 
                            OR IncidentDescription LIKE @Criteria";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Criteria", $"%{criteria}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incidents.Add(new Incident(
                                (int)reader["IncidentID"],
                                reader["IncidentType"].ToString(),
                                (DateTime)reader["IncidentDate"],
                                reader["IncidentLocation"].ToString(),
                                reader["IncidentDescription"].ToString(),
                                reader["IncidentStatus"].ToString(),
                                (int)reader["VictimID"],
                                (int)reader["SuspectID"]
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching incidents: {ex.Message}");
            }


            return incidents;
        }

        public Report GenerateIncidentReport(Report report)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;

            string checkReportQuery = "SELECT COUNT(*) FROM Reports WHERE ReportID = @ReportID";
            string checkIncidentQuery = "SELECT COUNT(*) FROM Incidents WHERE IncidentID = @IncidentID";
            string checkOfficerQuery = "SELECT COUNT(*) FROM Officers WHERE OfficerID = @OfficerID";
            string insertQuery = @"INSERT INTO Reports 
                         (ReportID, IncidentID, ReportingOfficer, ReportDate, ReportDetails, ReportStatus)
                         VALUES 
                         (@ReportID, @IncidentID, @OfficerID, @Date, @Details, @Status)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(checkReportQuery, con);
                    cmd.Parameters.AddWithValue("@ReportID", report.ReportID);
                    int reportCount = (int)cmd.ExecuteScalar();

                    if (reportCount > 0)
                    {
                        throw new DuplicateIdException($"Report with ID {report.ReportID} already exists");
                    }

                    cmd = new SqlCommand(checkIncidentQuery, con);
                    cmd.Parameters.AddWithValue("@IncidentID", report.IncidentID);
                    int incidentCount = (int)cmd.ExecuteScalar();

                    if (incidentCount == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {report.IncidentID} not found.");
                    }

                    cmd = new SqlCommand(checkOfficerQuery, con);
                    cmd.Parameters.AddWithValue("@OfficerID", report.ReportingOfficer);
                    int officerCount = (int)cmd.ExecuteScalar();

                    if (officerCount == 0)
                    {
                        throw new OfficerNotFoundException($"Officer with ID {report.ReportingOfficer} not found");
                    }

                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@ReportID", report.ReportID);
                    cmd.Parameters.AddWithValue("@IncidentID", report.IncidentID);
                    cmd.Parameters.AddWithValue("@OfficerID", report.ReportingOfficer);
                    cmd.Parameters.AddWithValue("@Date", report.ReportDate);
                    cmd.Parameters.AddWithValue("@Details", report.ReportDetails);
                    cmd.Parameters.AddWithValue("@Status", report.Status);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Failed to create report.");
                    }
                }

                return report;
            }
            catch (DuplicateIdException)
            {
                throw;
            }
            catch (IncidentNumberNotFoundException)
            {
                throw;
            }
            catch (OfficerNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating report: {ex.Message}");
                return null;
            }
        }

        public Case CreateCase(string caseDescription, List<Incident> incidents, int caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;

            string checkCaseQuery = "SELECT COUNT(*) FROM Cases WHERE CaseID = @CaseID";
            string insertQuery = @"INSERT INTO Cases 
                            (CaseID, CaseDescription, IncidentIDs) 
                            VALUES 
                            (@ID, @Desc, @IncidentIDs)";

            try
            {
                foreach (var incident in incidents)
                {
                    if (!IncidentExists(incident.IncidentID))
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {incident.IncidentID} not found");
                    }
                }

                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(checkCaseQuery, con);
                    cmd.Parameters.AddWithValue("@CaseID", caseId);
                    if ((int)cmd.ExecuteScalar() > 0)
                    {
                        throw new DuplicateIdException($"Case with ID {caseId} already exists");
                    }

                    string incidentIds = string.Join(",", incidents.Select(i => i.IncidentID));
                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@ID", caseId);
                    cmd.Parameters.AddWithValue("@Desc", caseDescription);
                    cmd.Parameters.AddWithValue("@IncidentIDs", incidentIds);
                    cmd.ExecuteNonQuery();
                }

                return new Case
                {
                    CaseID = caseId,
                    CaseDescription = caseDescription,
                    IncidentIDs = incidents,
                    CreatedDate = DateTime.Now
                };
            }
            catch (DuplicateIdException)
            {
                throw;
            }
            catch (IncidentNumberNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating case: {ex.Message}");
                return null;
            }
        }

        public Case GetCaseDetails(int caseId)
        {
            Case caseObj = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    string sql = "SELECT CaseDescription, IncidentIDs FROM Cases WHERE CaseID = @ID";
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", caseId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string incidentIds = reader["IncidentIDs"].ToString();
                            var incidents = new List<Incident>();

                            foreach (string id in incidentIds.Split(','))
                            {
                                if (int.TryParse(id, out int incidentId))
                                {
                                    var incident = GetIncident(incidentId);
                                    if (incident != null)
                                    {
                                        incidents.Add(incident);
                                    }
                                }
                            }

                            caseObj = new Case
                            {
                                CaseID = caseId,
                                CaseDescription = reader["CaseDescription"].ToString(),
                                IncidentIDs = incidents
                            };
                        }
                    }
                }
                return caseObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting case: {ex.Message}");
                return null;
            }
        }

        public Case UpdateCaseDetails(Case caseObj)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlCommand checkCmd = null;

            string checkCaseQuery = "SELECT COUNT(*) FROM Cases WHERE CaseID = @CaseID";
            string updateQuery = @"UPDATE Cases 
                         SET CaseDescription = @Desc, 
                             IncidentIDs = @IncidentIDs
                         WHERE CaseID = @ID";

            try
            {
                foreach (var incident in caseObj.IncidentIDs)
                {
                    if (!IncidentExists(incident.IncidentID))
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {incident.IncidentID} not found");
                    }
                }

                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(checkCaseQuery, con);
                    cmd.Parameters.AddWithValue("@CaseID", caseObj.CaseID);
                    if ((int)cmd.ExecuteScalar() == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Case with ID {caseObj.CaseID} not found");
                    }

                    var duplicateGroups = caseObj.IncidentIDs
                        .GroupBy(i => i.IncidentID)
                        .Where(g => g.Count() > 1);

                    if (duplicateGroups.Any())
                    {
                        throw new DuplicateIdException($"Duplicate incident IDs found: {string.Join(", ", duplicateGroups.Select(g => g.Key))}");
                    }

                    string incidentIds = string.Join(",", caseObj.IncidentIDs.Select(i => i.IncidentID));
                    cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@Desc", caseObj.CaseDescription);
                    cmd.Parameters.AddWithValue("@IncidentIDs", incidentIds);
                    cmd.Parameters.AddWithValue("@ID", caseObj.CaseID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No rows were updated");
                    }

                    return caseObj;
                }
            }
            catch (DuplicateIdException)
            {
                throw;
            }
            catch (IncidentNumberNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating case: {ex.Message}");
                throw;
            }

        }

        public List<Case> GetAllCases()
        {
            List<Case> cases = new List<Case>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    string sql = "SELECT CaseID, CaseDescription, IncidentIDs FROM Cases";
                    cmd = new SqlCommand(sql, con);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string incidentIds = reader["IncidentIDs"].ToString();
                            var incidents = new List<Incident>();

                            foreach (string id in incidentIds.Split(','))
                            {
                                if (int.TryParse(id, out int incidentId))
                                {
                                    var incident = GetIncident(incidentId);
                                    if (incident != null)
                                    {
                                        incidents.Add(incident);
                                    }
                                }
                            }

                            cases.Add(new Case
                            {
                                CaseID = (int)reader["CaseID"],
                                CaseDescription = reader["CaseDescription"].ToString(),
                                IncidentIDs = incidents
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting cases: {ex.Message}");
            }

            return cases;
        }

        // Helper methods
        private bool IncidentExists(int incidentId)
        {
            using (var con = DBUtility.GetConnection())
            {
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Incidents WHERE IncidentID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", incidentId);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private Incident GetIncident(int incidentId)
        {
            using (var con = DBUtility.GetConnection())
            {
                var cmd = new SqlCommand("SELECT * FROM Incidents WHERE IncidentID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", incidentId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Incident(
                            (int)reader["IncidentID"],
                            reader["IncidentType"].ToString(),
                            (DateTime)reader["IncidentDate"],
                            reader["IncidentLocation"].ToString(),
                            reader["IncidentDescription"].ToString(),
                            reader["IncidentStatus"].ToString(),
                            (int)reader["VictimID"],
                            (int)reader["SuspectID"]
                        );
                    }
                }
            }
            return null;
        }
    }
}
