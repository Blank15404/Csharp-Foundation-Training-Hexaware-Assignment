using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.Interfaces
{
    public interface IEnrollmentServiceDao
    {
        void AddEnrollment(Enrollment enrollment);
        void UpdateEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int enrollmentId);
        Enrollment GetEnrollmentById(int enrollmentId);
        List<Enrollment> GetAllEnrollments();
    }
}