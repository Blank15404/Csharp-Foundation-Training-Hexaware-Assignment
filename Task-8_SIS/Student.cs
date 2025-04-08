using System;
using System.Collections.Generic;

namespace Task_8_SIS
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

        public Student(int studentId, string firstName, string lastName, DateTime dob, string email, string phone)
        {
            StudentID = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }

        public void EnrollInCourse(Course course, DateTime enrollmentDate)
        {
            Enrollments.Add(new Enrollment(Enrollments.Count + 1, this, course, enrollmentDate));
        }

        public void UpdateInfo(string firstName, string lastName, DateTime dob, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dob;
            Email = email;
            PhoneNumber = phone;
        }

        public void MakePayment(decimal amount, DateTime paymentDate)
        {
            Payments.Add(new Payment(Payments.Count + 1, this, amount, paymentDate));
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Student ID: {StudentID}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"DOB: {DateOfBirth:d}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {PhoneNumber}");
        }

        public List<Course> GetEnrolledCourses()
        {
            List<Course> courses = new List<Course>();
            foreach (var enrollment in Enrollments)
            {
                courses.Add(enrollment.Course);
            }
            return courses;
        }

        public List<Payment> GetPaymentHistory() => Payments;
    }
}
