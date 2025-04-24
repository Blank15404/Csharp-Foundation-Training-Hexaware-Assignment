using System;
using System.Collections.Generic;
using System.Linq;
using CARS_CaseStudy.entity;

namespace CARS_CaseStudy
{
    public class UserInterface
    {
        public void DisplayMenu()
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
        }

        public int GetIncidentId()
        {
            Console.Write("Enter Incident ID: ");
            return int.Parse(Console.ReadLine());
        }

        public string GetIncidentType()
        {
            Console.Write("Enter Incident Type: ");
            return Console.ReadLine();
        }

        public DateTime GetIncidentDate()
        {
            Console.Write("Enter Incident Date (yyyy-mm-dd): ");
            return DateTime.Parse(Console.ReadLine());
        }

        public string GetLocation()
        {
            Console.Write("Enter Location: ");
            return Console.ReadLine();
        }

        public string GetDescription()
        {
            Console.Write("Enter Description: ");
            return Console.ReadLine();
        }

        public string GetStatus(string prompt = "Enter Status: ")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public int GetVictimId()
        {
            Console.Write("Enter Victim ID: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetSuspectId()
        {
            Console.Write("Enter Suspect ID: ");
            return int.Parse(Console.ReadLine());
        }

        public DateTime GetStartDate()
        {
            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            return DateTime.Parse(Console.ReadLine());
        }

        public DateTime GetEndDate()
        {
            Console.Write("Enter End Date (yyyy-mm-dd): ");
            return DateTime.Parse(Console.ReadLine());
        }

        public string GetSearchCriteria()
        {
            Console.Write("Enter Search Criteria: ");
            return Console.ReadLine();
        }

        public int GetReportId()
        {
            Console.Write("Enter Report ID: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetOfficerId()
        {
            Console.Write("Enter Reporting Officer ID: ");
            return int.Parse(Console.ReadLine());
        }

        public DateTime GetReportDate()
        {
            Console.Write("Enter Report Date (yyyy-mm-dd): ");
            return DateTime.Parse(Console.ReadLine());
        }

        public string GetReportDetails()
        {
            Console.Write("Enter Report Details: ");
            return Console.ReadLine();
        }

        public string GetReportStatus()
        {
            Console.Write("Enter Report Status (Draft/Finalized): ");
            return Console.ReadLine();
        }

        public int GetCaseId()
        {
            Console.Write("Enter Case ID: ");
            return int.Parse(Console.ReadLine());
        }

        public string GetCaseDescription(string prompt = "Enter Case Description: ")
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public List<int> GetIncidentIds()
        {
            Console.Write("Enter Incident IDs to link (comma separated): ");
            return Console.ReadLine()
                .Split(',')
                .Select(id => int.Parse(id.Trim()))
                .ToList();
        }

        public void DisplayIncidents(List<Incident> incidents)
        {
            if (incidents.Count == 0)
            {
                Console.WriteLine("No incidents found.");
                return;
            }

            foreach (var incident in incidents)
            {
                Console.WriteLine($"ID: {incident.IncidentID}, Type: {incident.IncidentType}, " +
                                $"Date: {incident.IncidentDate}, Status: {incident.Status}");
            }
        }

        public void DisplayCaseDetails(Case caseDetails)
        {
            if (caseDetails == null)
            {
                Console.WriteLine("Case not found.");
                return;
            }

            Console.WriteLine($"\nCase ID: {caseDetails.CaseID}");
            Console.WriteLine($"Description: {caseDetails.CaseDescription}");

            if (caseDetails.IncidentIDs.Count > 0)
            {
                Console.WriteLine("Linked Incident IDs: " +
                    string.Join(", ", caseDetails.IncidentIDs.Select(i => i.IncidentID)));
            }
            else
            {
                Console.WriteLine("No incidents linked to this case.");
            }
        }

        public void DisplayAllCases(List<Case> cases)
        {
            if (cases.Count == 0)
            {
                Console.WriteLine("No cases found.");
                return;
            }

            Console.WriteLine("\nAll Cases:");
            foreach (var c in cases)
            {
                Console.WriteLine($"{c.CaseID}: {c.CaseDescription}");
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {error}");
            Console.ResetColor();
        }
    }
}
