using System;
using System.Collections.Generic;

namespace StudentInformationSystem.entity
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public Student() { }

        public Student(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentID = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void EnrollInCourse(Course course, DateTime enrollmentDate)
        {
            Enrollments.Add(new Enrollment
            {
                StudentID = this.StudentID,
                CourseID = course.CourseID,
                EnrollmentDate = enrollmentDate
            });
        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            Payments.Add(new Payment
            {
                StudentID = this.StudentID,
                Amount = amount,
                PaymentDate = paymentDate
            });
        }
    }
}