using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CARS_CaseStudy.util;
using CARS_CaseStudy.exception;
using CARS_CaseStudy.entity;

namespace CARS_CaseStudy.dao
{
    public class CrimeAnalysisServiceImpl : ICrimeAnalysisService
    {
        private static SqlConnection connection;

        public CrimeAnalysisServiceImpl()
        {
            string connectionString = DBPropertyUtil.GetPropertyString("database.properties");
            connection = DBConnUtil.GetConnection(connectionString);
        }

        public bool CreateIncident(Incident incident)
        {
            try
            {
                string query = "INSERT INTO Incidents (IncidentID, IncidentType, IncidentDate, IncidentLocation, IncidentDescription, IncidentStatus, VictimID, SuspectID) " +
                    "VALUES (@IncidentID, @IncidentType, @IncidentDate, @Location, @Description, @Status, @VictimID, @SuspectID)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);
                    cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                    cmd.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
                    cmd.Parameters.AddWithValue("@Location", incident.Location);
                    cmd.Parameters.AddWithValue("@Description", incident.Description);
                    cmd.Parameters.AddWithValue("@Status", incident.Status);
                    cmd.Parameters.AddWithValue("@VictimID", incident.VictimID);
                    cmd.Parameters.AddWithValue("@SuspectID", incident.SuspectID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating incident: {ex.Message}");
                return false;
            }
        }

        public bool UpdateIncidentStatus(string status, int incidentId)
        {
            try
            {
                string query = "UPDATE Incidents SET IncidentStatus = @Status WHERE IncidentID = @IncidentID";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@IncidentID", incidentId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {incidentId} not found.");
                    }
                    return rowsAffected > 0;
                }
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
            List<Incident> incidents = new List<Incident>();

            try
            {
                string query = "SELECT * FROM Incidents WHERE IncidentDate BETWEEN @StartDate AND @EndDate";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
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

            try
            {
                string query = "SELECT * FROM Incidents WHERE IncidentType LIKE @Criteria OR IncidentDescription LIKE @Criteria";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
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
            try
            {
                string verifyQuery = "SELECT COUNT(*) FROM Incidents WHERE IncidentID = @IncidentID";
                using (SqlCommand verifyCmd = new SqlCommand(verifyQuery, connection))
                {
                    verifyCmd.Parameters.AddWithValue("@IncidentID", report.IncidentID);
                    int count = (int)verifyCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        throw new IncidentNumberNotFoundException($"Incident with ID {report.IncidentID} not found.");
                    }
                }

                string query = @"INSERT INTO Reports 
                       (ReportID, IncidentID, ReportingOfficer, ReportDate, ReportDetails, ReportStatus)
                       VALUES 
                       (@ReportID, @IncidentID, @OfficerID, @Date, @Details, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
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
            catch (IncidentNumberNotFoundException)
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
            try
            {
                string sql = "INSERT INTO Cases (CaseID, Description) VALUES (@ID, @Desc)";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", caseId);
                    cmd.Parameters.AddWithValue("@Desc", caseDescription);
                    cmd.ExecuteNonQuery();
                }

                foreach (var incident in incidents)
                {
                    sql = "INSERT INTO CaseIncidents VALUES (@CaseID, @IncidentID)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@CaseID", caseId);
                        cmd.Parameters.AddWithValue("@IncidentID", incident.IncidentID);
                        cmd.ExecuteNonQuery();
                    }
                }

                return new Case
                {
                    CaseID = caseId,
                    CaseDescription = caseDescription,
                    Incidents = incidents
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating case: {ex.Message}");
                return null;
            }
        }


        public Case GetCaseDetails(int caseId)
        {
            try
            {
                string sql = "SELECT Description FROM Cases WHERE CaseID = @ID";
                string description;

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", caseId);
                    description = cmd.ExecuteScalar()?.ToString();
                }

                if (description == null) return null;

                var incidents = new List<Incident>();
                sql = "SELECT IncidentID FROM CaseIncidents WHERE CaseID = @ID";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", caseId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            incidents.Add(new Incident
                            {
                                IncidentID = (int)reader["IncidentID"]
                            });
                        }
                    }
                }

                return new Case
                {
                    CaseID = caseId,
                    CaseDescription = description,
                    Incidents = incidents
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting case: {ex.Message}");
                return null;
            }
        }

        public bool UpdateCaseDetails(Case caseObj)
        {
            try
            {
                string sql = "UPDATE Cases SET Description = @Desc WHERE CaseID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Desc", caseObj.CaseDescription);
                    cmd.Parameters.AddWithValue("@ID", caseObj.CaseID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating case: {ex.Message}");
                return false;
            }
        }

        public List<Case> GetAllCases()
        {
            var cases = new List<Case>();
            try
            {
                string sql = "SELECT CaseID, Description FROM Cases";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cases.Add(new Case
                            {
                                CaseID = (int)reader["CaseID"],
                                CaseDescription = reader["Description"].ToString()
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
    }
}
