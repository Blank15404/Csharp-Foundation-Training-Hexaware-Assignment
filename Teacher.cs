using System.Collections.Generic;
using Task_8_SIS;

namespace Task_8_SIS
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherID = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateInfo(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Teacher ID: {TeacherID}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
        }

        public List<Course> GetAssignedCourses() => AssignedCourses;
    }
}