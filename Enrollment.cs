using System;

namespace Task_8_SIS
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int id, Student student, Course course, DateTime date)
        {
            EnrollmentID = id;
            Student = student;
            Course = course;
            EnrollmentDate = date;
        }

        public Student GetStudent() => Student;
        public Course GetCourse() => Course;
    }
}