using System.Collections.Generic;

namespace StudentInformationSystem.entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int InstructorId { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public Course() { }

        public Course(int courseId, string courseName, string courseCode, int instructorId)
        {
            CourseID = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
            InstructorId = instructorId;
        }
    }
}
