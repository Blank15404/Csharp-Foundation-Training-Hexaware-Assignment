using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.Interfaces
{
    public interface IStudentServiceDao
    {
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);
        Student GetStudentById(int studentId);
        List<Student> GetAllStudents();
        void EnrollStudentInCourse(int studentId, int courseId, DateTime enrollmentDate);
        List<Course> GetEnrolledCourses(int studentId);
        void MakePayment(int studentId, decimal amount, DateTime paymentDate);
        List<Payment> GetPaymentHistory(int studentId);
    }
}