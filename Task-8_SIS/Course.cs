using System.Collections.Generic;

namespace Task_8_SIS
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public Teacher Instructor { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public Course(int courseId, string name, string code)
        {
            CourseID = courseId;
            CourseName = name;
            CourseCode = code;
        }

        public void AssignTeacher(Teacher teacher)
        {
            Instructor = teacher;
            teacher.AssignedCourses.Add(this);
        }

        public void UpdateInfo(string code, string name, Teacher instructor)
        {
            CourseCode = code;
            CourseName = name;
            Instructor = instructor;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}");
            Console.WriteLine($"Name: {CourseName}");
            Console.WriteLine($"Code: {CourseCode}");
            Console.WriteLine($"Instructor: {Instructor?.FirstName} {Instructor?.LastName}");
        }

        public List<Enrollment> GetEnrollments() => Enrollments;
        public Teacher GetTeacher() => Instructor;
    }
}
