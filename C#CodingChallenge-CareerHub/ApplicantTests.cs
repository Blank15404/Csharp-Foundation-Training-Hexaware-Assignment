using NUnit.Framework;
using CareerHub.dao;
using CareerHub.entity;
using System;

namespace CareerHub.Tests
{
    [TestFixture]
    public class ApplicantTests
    {
        private IApplicantDao applicantDao;
        private const int TestApplicantId = 9999;

        [SetUp]
        public void Setup()
        {
            applicantDao = new ApplicantDaoImpl();
            try { applicantDao.GetApplicantById(TestApplicantId); }
            catch {  }
        }

        [Test]
        public void AddApplicantValidDataReturnsOneRowAffected()
        {
            // Arrange
            var applicant = new Applicant(
                TestApplicantId,
                "Test",
                "User",
                "test@example.com",
                "9876543210",
                "Test_Resume.pdf");

            // Act
            int result = applicantDao.AddApplicant(applicant);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AddApplicantValidDataCanRetrieveAddedApplicant()
        {
            // Arrange
            var applicant = new Applicant(
                TestApplicantId,
                "Test",
                "User",
                "test@example.com",
                "9876543210",
                "Test_Resume.pdf");

            // Act
            applicantDao.AddApplicant(applicant);
            var retrieved = applicantDao.GetApplicantById(TestApplicantId);

            // Assert
            Assert.AreEqual("Test", retrieved.FirstName);
            Assert.AreEqual("User", retrieved.LastName);
            Assert.AreEqual("test@example.com", retrieved.Email);
        }

        [Test]
        public void AddApplicantDuplicateIdThrowsException()
        {
            // Arrange
            var applicant1 = new Applicant(
                TestApplicantId,
                "Test",
                "User",
                "test@example.com",
                "9876543210",
                "Test_Resume.pdf");

            var applicant2 = new Applicant(
                TestApplicantId,
                "Duplicate",
                "User",
                "duplicate@example.com",
                "9876543211",
                "Duplicate_Resume.pdf");

            // Act
            applicantDao.AddApplicant(applicant1);

            // Assert
            var ex = Assert.Throws<Exception>(() => applicantDao.AddApplicant(applicant2));
            Assert.That(ex.Message, Does.Contain("already exists"));
        }

    }
}