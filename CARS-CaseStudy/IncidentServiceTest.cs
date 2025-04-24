using NUnit.Framework;
using CARS_CaseStudy.dao;
using CARS_CaseStudy.entity;
using CARS_CaseStudy.exception;

namespace CARSTestProject
{
    [TestFixture]
    public class IncidentServiceTests
    {
        private ICrimeAnalysisService incidentService;

        [SetUp]
        public void Setup()
        {
            incidentService = new CrimeAnalysisServiceImpl();
        }

        [Test]
        public void CreateIncidentValidDataReturnsTrue()
        {
            var incident = new Incident(3007, "Theft", new DateTime(2023, 1, 1),
                           "Main St", "Stolen wallet", "Open", 1001, 2001);

            bool result = incidentService.CreateIncident(incident);
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateIncidentStatusValidIdReturnsTrue()
        {
            incidentService.CreateIncident(
                new Incident(3002, "Burglary", new DateTime(2023, 1, 2),
                "Park Ave", "Broken window", "Open", 1001, 2001));

            bool result = incidentService.UpdateIncidentStatus("Closed", 3002);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetIncidentsInDateRangeReturnsCorrectIncidents()
        {
            incidentService.CreateIncident(
                new Incident(3003, "Vandalism", new DateTime(2023, 1, 3),
                "School", "Graffiti", "Open", 1001, 2001));

            var incidents = incidentService.GetIncidentsInDateRange(
                new DateTime(2023, 1, 1),
                new DateTime(2023, 1, 31));

            Assert.IsNotEmpty(incidents);
        }

        [Test]
        public void SearchIncidentsReturnsMatchingResults()
        {
            incidentService.CreateIncident(
                new Incident(3004, "Assault", new DateTime(2023, 1, 4),
                "Bar", "Bar fight", "Open", 1001, 2001));

            var results = incidentService.SearchIncidents("Assault");
            Assert.IsNotEmpty(results);
        }
    }
}
