using System.Collections.Generic;

namespace StudentInformationSystem.entity
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public Teacher() { }

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherID = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}