using System;
using System.Collections.Generic;
using System.Linq;
using CARS_CaseStudy.dao;
using CARS_CaseStudy.entity;
using CARS_CaseStudy.exception;

namespace CARS_CaseStudy
{
    class Program
    {
        private static ICrimeAnalysisService service = new CrimeAnalysisServiceImpl();
        private static UserInterface ui = new UserInterface();

        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    ui.DisplayMenu();
                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int choice))
                    {
                        ui.DisplayError("Please enter a valid number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            CreateIncident();
                            break;
                        case 2:
                            UpdateIncidentStatus();
                            break;
                        case 3:
                            GetIncidentsInDateRange();
                            break;
                        case 4:
                            SearchIncidents();
                            break;
                        case 5:
                            GenerateIncidentReport();
                            break;
                        case 6:
                            CreateCase();
                            break;
                        case 7:
                            GetCaseDetails();
                            break;
                        case 8:
                            UpdateCaseDetails();
                            break;
                        case 9:
                            GetAllCases();
                            break;
                        case 0:
                            return;
                        default:
                            ui.DisplayError("Invalid choice. Please select a valid option (0-9).");
                            break;
                    }
                }
                catch (FormatException)
                {
                    ui.DisplayError("Invalid input format. Please enter a valid number.");
                }
                catch (IncidentNumberNotFoundException ex)
                {
                    ui.DisplayError(ex.Message);
                }
                catch (Exception ex)
                {
                    ui.DisplayError($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        static void CreateIncident()
        {
            int incidentId = ui.GetIncidentId();
            string incidentType = ui.GetIncidentType();
            DateTime incidentDate = ui.GetIncidentDate();
            string location = ui.GetLocation();
            string description = ui.GetDescription();
            string status = ui.GetStatus();
            int victimId = ui.GetVictimId();
            int suspectId = ui.GetSuspectId();

            Incident incident = new Incident(incidentId, incidentType, incidentDate,
                                          location, description, status, victimId, suspectId);

            bool result = service.CreateIncident(incident);
            ui.DisplayMessage(result ? "Incident created successfully!" : "Failed to create incident.");
        }

        static void UpdateIncidentStatus()
        {
            int incidentId = ui.GetIncidentId();
            string status = ui.GetStatus("Enter new status: ");

            bool result = service.UpdateIncidentStatus(status, incidentId);
            ui.DisplayMessage(result ? "Incident status updated successfully!" : "Failed to update incident status.");
        }

        static void GetIncidentsInDateRange()
        {
            DateTime startDate = ui.GetStartDate();
            DateTime endDate = ui.GetEndDate();

            List<Incident> incidents = service.GetIncidentsInDateRange(startDate, endDate);
            ui.DisplayIncidents(incidents);
        }

        static void SearchIncidents()
        {
            string criteria = ui.GetSearchCriteria();
            List<Incident> incidents = service.SearchIncidents(criteria);
            ui.DisplayIncidents(incidents);
        }

        static void GenerateIncidentReport()
        {
            int reportId = ui.GetReportId();
            int incidentId = ui.GetIncidentId();
            int officerId = ui.GetOfficerId();
            DateTime reportDate = ui.GetReportDate();
            string details = ui.GetReportDetails();
            string status = ui.GetReportStatus();

            Report report = new Report
            {
                ReportID = reportId,
                IncidentID = incidentId,
                ReportingOfficer = officerId,
                ReportDate = reportDate,
                ReportDetails = details,
                Status = status
            };

            Report createdReport = service.GenerateIncidentReport(report);
            ui.DisplayMessage(createdReport != null ? "Report generated successfully!" : "Failed to generate report.");
        }

        static void CreateCase()
        {
            int caseId = ui.GetCaseId();
            string description = ui.GetCaseDescription();
            List<int> incidentIds = ui.GetIncidentIds();

            var incidents = incidentIds.Select(id => new Incident { IncidentID = id }).ToList();
            Case newCase = service.CreateCase(description, incidents, caseId);

            ui.DisplayMessage(newCase != null ? "Case created successfully!" : "Failed to create case.");
        }

        static void GetCaseDetails()
        {
            int caseId = ui.GetCaseId();
            Case caseDetails = service.GetCaseDetails(caseId);
            ui.DisplayCaseDetails(caseDetails);
        }

        static void UpdateCaseDetails()
        {
            int caseId = ui.GetCaseId();
            string newDescription = ui.GetCaseDescription("Enter new description: ");
            List<int> incidentIds = ui.GetIncidentIds();

            try
            {
                var result = service.UpdateCaseDetails(new Case
                {
                    CaseID = caseId,
                    CaseDescription = newDescription,
                    IncidentIDs = incidentIds.Select(id => new Incident { IncidentID = id }).ToList()
                });

                ui.DisplayMessage("Case updated successfully!");
            }
            catch
            {
                ui.DisplayMessage("Failed to update case.");
            }
        }

        static void GetAllCases()
        {
            List<Case> cases = service.GetAllCases();
            ui.DisplayAllCases(cases);
        }
    }
}
