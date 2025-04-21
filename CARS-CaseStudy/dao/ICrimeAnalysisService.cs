using System;
using System.Collections.Generic;
using CARS_CaseStudy.entity;

namespace CARS_CaseStudy.dao
{
    public interface ICrimeAnalysisService
    {
        bool CreateIncident(Incident incident);
        bool UpdateIncidentStatus(string status, int incidentId);
        List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);
        List<Incident> SearchIncidents(string criteria);
        Report GenerateIncidentReport(Report report);
        Case CreateCase(string caseDescription, List<Incident> incidents, int caseId);
        Case GetCaseDetails(int caseId);
        bool UpdateCaseDetails(Case caseObj);
        List<Case> GetAllCases();
    }
}