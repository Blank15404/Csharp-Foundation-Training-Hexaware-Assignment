namespace StudentInformationSystem.entity
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public System.DateTime EnrollmentDate { get; set; }

        public Enrollment() { }

        public Enrollment(int enrollmentId, int studentId, int courseId, System.DateTime enrollmentDate)
        {
            EnrollmentID = enrollmentId;
            StudentID = studentId;
            CourseID = courseId;
            EnrollmentDate = enrollmentDate;
        }
    }
}