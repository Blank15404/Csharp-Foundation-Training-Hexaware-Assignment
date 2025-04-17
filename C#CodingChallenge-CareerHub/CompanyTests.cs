using NUnit.Framework;
using CareerHub.dao;
using CareerHub.entity;
using System;

namespace CareerHub.Tests
{
    [TestFixture]
    public class CompanyTests
    {
        private ICompanyDao companyDao;
        private const int TestCompanyId = 9999;

        [SetUp]
        public void Setup()
        {
            companyDao = new CompanyDaoImpl();
            try { companyDao.GetCompanyById(TestCompanyId); }
            catch {  }
        }

        [Test]
        public void AddCompanyValidDataReturnsOneRowAffected()
        {
            // Arrange
            var company = new Company(
                TestCompanyId,
                "Test Company",
                "Test Location");

            // Act
            int result = companyDao.AddCompany(company);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AddCompanyValidDataCanRetrieveAddedCompany()
        {
            // Arrange
            var company = new Company(
                TestCompanyId,
                "Test Company",
                "Test Location");

            // Act
            companyDao.AddCompany(company);
            var retrieved = companyDao.GetCompanyById(TestCompanyId);

            // Assert
            Assert.AreEqual("Test Company", retrieved.CompanyName);
            Assert.AreEqual("Test Location", retrieved.Location);
        }

        [Test]
        public void AddCompanyDuplicateIdThrowsException()
        {
            // Arrange
            var company1 = new Company(
                TestCompanyId,
                "Test Company",
                "Test Location");

            var company2 = new Company(
                TestCompanyId,
                "Duplicate Company",
                "Duplicate Location");

            // Act
            companyDao.AddCompany(company1);

            // Assert
            var ex = Assert.Throws<Exception>(() => companyDao.AddCompany(company2));
            Assert.That(ex.Message, Does.Contain("already exists"));
        }

        [Test]
        public void AddCompanyEmptyNameThrowsException()
        {
            // Arrange
            var company = new Company(
                TestCompanyId,
                "", // Empty name
                "Test Location");

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => companyDao.AddCompany(company));
            Assert.That(ex.Message, Does.Contain("Company").IgnoreCase);
        }
    }
}