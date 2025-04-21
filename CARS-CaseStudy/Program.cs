using System;
using System.Collections.Generic;
using CARS_CaseStudy.dao;
using CARS_CaseStudy.entity;
using CARS_CaseStudy.exception;

namespace CARS_CaseStudy
{
    class Program
    {
        private static ICrimeAnalysisService service = new CrimeAnalysisServiceImpl();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nCrime Analysis and Reporting System (C.A.R.S.)");
                Console.WriteLine("1. Create Incident");
                Console.WriteLine("2. Update Incident Status");
                Console.WriteLine("3. Get Incidents in Date Range");
                Console.WriteLine("4. Search Incidents");
                Console.WriteLine("5. Generate Incident Report");
                Console.WriteLine("6. Create Case");
                Console.WriteLine("7. Get Case Details");
                Console.WriteLine("8. Update Case Details");
                Console.WriteLine("9. Get All Cases");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                try
                {
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
                            Console.WriteLine("Exiting system...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (IncidentNumberNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static void CreateIncident()
        {
            Console.WriteLine("\nCreate New Incident");

            Console.Write("Incident ID: ");
            int incidentId = int.Parse(Console.ReadLine());

            Console.Write("Incident Type: ");
            string incidentType = Console.ReadLine();

            Console.Write("Incident Date (yyyy-mm-dd): ");
            DateTime incidentDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Location: ");
            string location = Console.ReadLine();

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Status: ");
            string status = Console.ReadLine();

            Console.Write("Victim ID: ");
            int victimId = int.Parse(Console.ReadLine());

            Console.Write("Suspect ID: ");
            int suspectId = int.Parse(Console.ReadLine());

            Incident incident = new Incident(incidentId, incidentType, incidentDate, location,
                                           description, status, victimId, suspectId);

            bool result = service.CreateIncident(incident);
            Console.WriteLine(result ? "Incident created successfully!" : "Failed to create incident.");
        }

        static void UpdateIncidentStatus()
        {
            Console.WriteLine("\nUpdate Incident Status");

            Console.Write("Incident ID: ");
            int incidentId = int.Parse(Console.ReadLine());

            Console.Write("New Status: ");
            string status = Console.ReadLine();

            bool result = service.UpdateIncidentStatus(status, incidentId);
            Console.WriteLine(result ? "Incident status updated successfully!" : "Failed to update incident status.");
        }

        static void GetIncidentsInDateRange()
        {
            Console.WriteLine("\nGet Incidents in Date Range");

            Console.Write("Start Date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            List<Incident> incidents = service.GetIncidentsInDateRange(startDate, endDate);

            Console.WriteLine("\nIncidents Found:");
            foreach (var incident in incidents)
            {
                Console.WriteLine($"ID: {incident.IncidentID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}, Status: {incident.Status}");
            }
        }

        static void SearchIncidents()
        {
            Console.WriteLine("\nSearch Incidents");

            Console.Write("Search Criteria: ");
            string criteria = Console.ReadLine();

            List<Incident> incidents = service.SearchIncidents(criteria);

            Console.WriteLine("\nMatching Incidents:");
            foreach (var incident in incidents)
            {
                Console.WriteLine($"ID: {incident.IncidentID}, Type: {incident.IncidentType}, Description: {incident.Description}");
            }
        }

        static void GenerateIncidentReport()
        {
            Console.WriteLine("\nGenerate New Report");

            Console.Write("Report ID: ");
            int reportId = int.Parse(Console.ReadLine());

            Console.Write("Incident ID: ");
            int incidentId = int.Parse(Console.ReadLine());

            Console.Write("Reporting Officer ID: ");
            int officerId = int.Parse(Console.ReadLine());

            Console.Write("Report Date (yyyy-mm-dd): ");
            DateTime reportDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Report Details: ");
            string details = Console.ReadLine();

            Console.Write("Report Status (Draft/Finalized): ");
            string status = Console.ReadLine();

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
            Console.WriteLine(createdReport != null ? "Report generated successfully!" : "Failed to generate report.");
        }

        static void CreateCase()
        {
            Console.Write("Enter Case ID (number): ");
            int caseId = int.Parse(Console.ReadLine());

            Console.Write("Enter case description: ");
            string desc = Console.ReadLine();

            Console.Write("Enter Incident IDs to link (comma separated): ");
            var incidentIds = Console.ReadLine().Split(',')
                                  .Select(id => new Incident { IncidentID = int.Parse(id.Trim()) })
                                  .ToList();

            Case newCase = service.CreateCase(desc, incidentIds, caseId);
            Console.WriteLine(newCase != null ? "Case created!" : "Failed");
        }

        static void GetCaseDetails()
        {
            Console.Write("Enter Case ID to view: ");
            int caseId = int.Parse(Console.ReadLine());

            Case c = service.GetCaseDetails(caseId);
            if (c != null)
            {
                Console.WriteLine($"\nCase {c.CaseID}: {c.CaseDescription}");
                Console.WriteLine("Linked Incident IDs: " +
                    string.Join(", ", c.Incidents.Select(i => i.IncidentID)));
            }
            else
            {
                Console.WriteLine("Case not found");
            }
        }

        static void UpdateCaseDetails()
        {
            Console.Write("Enter Case ID to update: ");
            int caseId = int.Parse(Console.ReadLine());

            Console.Write("New description: ");
            string newDesc = Console.ReadLine();

            bool success = service.UpdateCaseDetails(new Case
            {
                CaseID = caseId,
                CaseDescription = newDesc
            });
            Console.WriteLine(success ? "Updated successfully!" : "Update failed");
        }

        static void GetAllCases()
        {
            Console.WriteLine("\nAll Cases:");
            var cases = service.GetAllCases();

            foreach (var c in cases)
            {
                Console.WriteLine($"{c.CaseID}: {c.CaseDescription}");
            }

            if (cases.Count == 0) Console.WriteLine("No cases found");
        }
    }
}