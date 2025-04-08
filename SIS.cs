using System;
using System.Collections.Generic;

namespace Task_8_SIS
{
    public class SIS
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public SIS()
        {
            // Hardcoded sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Add sample students
            Students.Add(new Student(1, "Manish", "Siva", new DateTime(2000, 5, 15), "manish@school.edu", "1234567890"));
            Students.Add(new Student(2, "Ismail", "Ali", new DateTime(2001, 3, 22), "ismail@school.edu", "2345678901"));
            Students.Add(new Student(3, "Rohan", "Panchal", new DateTime(1999, 8, 10), "rohan@school.edu", "3456789012"));

            // Add sample courses
            Courses.Add(new Course(101, "Mathematics", "MATH101"));
            Courses.Add(new Course(102, "Computer Science", "CS102"));
            Courses.Add(new Course(103, "Physics", "PHYS103"));

            // Add sample teachers
            Teachers.Add(new Teacher(1001, "Ram", "Charan", "ram@school.edu"));
            Teachers.Add(new Teacher(1002, "Hari", "Amar", "hari@school.edu"));
            Teachers.Add(new Teacher(1003, "Amar", "Nag", "amar@school.edu"));

            // Enrollments
            EnrollStudent(1, 101, new DateTime(2023, 9, 1));
            EnrollStudent(1, 102, new DateTime(2023, 9, 1));
            EnrollStudent(2, 102, new DateTime(2023, 9, 5));
            EnrollStudent(3, 103, new DateTime(2023, 9, 10));

            // Teacher Assignments
            AssignTeacherToCourse(1001, 101);
            AssignTeacherToCourse(1002, 102);
            AssignTeacherToCourse(1003, 103);

            // Payments
            AddPayment(1, 500.00m, new DateTime(2023, 8, 25));
            AddPayment(2, 750.00m, new DateTime(2023, 8, 28));
            AddPayment(3, 600.00m, new DateTime(2023, 8, 30));
        }

        public void AddStudent(int id, string firstName, string lastName, DateTime dob, string email, string phone)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new SISException("First name and last name are required");
            }
            if (dob > DateTime.Now)
            {
                throw new SISException("Date of birth cannot be in the future");
            }
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                throw new SISException("Valid email is required");
            }

            Students.Add(new Student(id, firstName, lastName, dob, email, phone));
        }

        public void AddCourse(int id, string name, string code = "CS101")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new SISException("Course name is required");
            }

            Courses.Add(new Course(id, name, code));
        }

        public void AddTeacher(int id, string firstName, string lastName, string email = "teacher@school.edu")
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new SISException("First name and last name are required");
            }
            Teachers.Add(new Teacher(id, firstName, lastName, email));
        }

        public void EnrollStudent(int studentId, int courseId, DateTime enrollmentDate)
        {
            var student = Students.Find(s => s.StudentID == studentId);
            var course = Courses.Find(c => c.CourseID == courseId);

            if (student == null)
            {
                throw new SISException($"Student with ID {studentId} not found");
            }
            if (course == null)
            {
                throw new SISException($"Course with ID {courseId} not found");
            }
            if (student.Enrollments.Exists(e => e.Course.CourseID == courseId))
            {
                throw new SISException($"Student {studentId} is already enrolled in course {courseId}");
            }

            var enrollment = new Enrollment(Enrollments.Count + 1, student, course, enrollmentDate);
            Enrollments.Add(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

        public void AssignTeacherToCourse(int teacherId, int courseId)
        {
            var teacher = Teachers.Find(t => t.TeacherID == teacherId);
            var course = Courses.Find(c => c.CourseID == courseId);

            if (teacher == null)
            {
                throw new SISException($"Teacher with ID {teacherId} not found");
            }
            if (course == null)
            {
                throw new SISException($"Course with ID {courseId} not found");
            }

            course.AssignTeacher(teacher);
        }

        public void AddPayment(int studentId, decimal amount, DateTime paymentDate)
        {
            var student = Students.Find(s => s.StudentID == studentId);
            if (student == null)
            {
                throw new SISException($"Student with ID {studentId} not found");
            }
            if (amount <= 0)
            {
                throw new SISException("Payment amount must be positive");
            }

            var payment = new Payment(Payments.Count + 1, student, amount, paymentDate);
            Payments.Add(payment);
            student.Payments.Add(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(int studentId)
        {
            var student = Students.Find(s => s.StudentID == studentId);
            if (student == null)
            {
                throw new SISException($"Student with ID {studentId} not found");
            }
            return student.Enrollments;
        }

        public List<Course> GetCoursesForTeacher(int teacherId)
        {
            var teacher = Teachers.Find(t => t.TeacherID == teacherId);
            if (teacher == null)
            {
                throw new SISException($"Teacher with ID {teacherId} not found");
            }
            return teacher.AssignedCourses;
        }

        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine($"Enrollment Report for {course.CourseName}:");
            foreach (var e in course.Enrollments)
            {
                Console.WriteLine($"- {e.Student.FirstName} {e.Student.LastName} (Enrolled: {e.EnrollmentDate:d})");
            }
        }

        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine($"Payment Report for {student.FirstName} {student.LastName}:");
            foreach (var p in student.Payments)
            {
                Console.WriteLine($"- ${p.Amount} on {p.PaymentDate:d}");
            }
        }

        public void CalculateCourseStatistics(Course course)
        {
            Console.WriteLine($"Statistics for {course.CourseName}:");
            Console.WriteLine($"- Enrollments: {course.Enrollments.Count}");
            decimal totalPayments = 0;
            foreach (var e in course.Enrollments)
            {
                foreach (var p in e.Student.Payments)
                {
                    totalPayments += p.Amount;
                }
            }
            Console.WriteLine($"- Total Payments: ${totalPayments}");
        }
    }
}