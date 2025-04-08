using System;
using System.Collections.Generic;

namespace SIS
{
    // Student Class
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

        public Student(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentID = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        // Student Methods
        public void EnrollInCourse(Course course, DateTime enrollmentDate, SIS sis)
        {
            var enrollment = new Enrollment(sis.Enrollments.Count + 1, this, course, enrollmentDate);
            Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
            sis.Enrollments.Add(enrollment);
        }

        public void UpdateStudentInfo(string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void MakePayment(decimal amount, DateTime paymentDate, SIS sis)
        {
            var payment = new Payment(sis.Payments.Count + 1, this, amount, paymentDate);
            Payments.Add(payment);
            sis.Payments.Add(payment);
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Student ID: {StudentID}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Date of Birth: {DateOfBirth.ToShortDateString()}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone: {PhoneNumber}");
        }

        public List<Course> GetEnrolledCourses()
        {
            var courses = new List<Course>();
            foreach (var enrollment in Enrollments)
            {
                courses.Add(enrollment.Course);
            }
            return courses;
        }

        public List<Payment> GetPaymentHistory()
        {
            return Payments;
        }
    }

    // Course Class
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public Teacher Instructor { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public Course(int courseId, string courseName, string courseCode)
        {
            CourseID = courseId;
            CourseName = courseName;
            CourseCode = courseCode;
        }

        // Course Methods
        public void AssignTeacher(Teacher teacher)
        {
            Instructor = teacher;
            teacher.Courses.Add(this);
        }

        public void UpdateCourseInfo(string courseCode, string courseName, Teacher instructor)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            Instructor = instructor;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}");
            Console.WriteLine($"Course Name: {CourseName}");
            Console.WriteLine($"Course Code: {CourseCode}");
            Console.WriteLine($"Instructor: {(Instructor != null ? $"{Instructor.FirstName} {Instructor.LastName}" : "Not assigned")}");
        }

        public List<Enrollment> GetEnrollments()
        {
            return Enrollments;
        }

        public Teacher GetTeacher()
        {
            return Instructor;
        }
    }

    // Enrollment Class
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int enrollmentId, Student student, Course course, DateTime enrollmentDate)
        {
            EnrollmentID = enrollmentId;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }

        // Enrollment Methods
        public Student GetStudent()
        {
            return Student;
        }

        public Course GetCourse()
        {
            return Course;
        }
    }

    // Teacher Class
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherID = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        // Teacher Methods
        public void UpdateTeacherInfo(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher ID: {TeacherID}");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
        }

        public List<Course> GetAssignedCourses()
        {
            return Courses;
        }
    }

    // Payment Class
    public class Payment
    {
        public int PaymentID { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(int paymentId, Student student, decimal amount, DateTime paymentDate)
        {
            PaymentID = paymentId;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        // Payment Methods
        public Student GetStudent()
        {
            return Student;
        }

        public decimal GetPaymentAmount()
        {
            return Amount;
        }

        public DateTime GetPaymentDate()
        {
            return PaymentDate;
        }
    }

    // SIS Class to manage the system
    public class SIS
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public SIS() { }

        // SIS Methods
        public void EnrollStudentInCourse(Student student, Course course, DateTime enrollmentDate)
        {
            student.EnrollInCourse(course, enrollmentDate, this);
        }

        public void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            course.AssignTeacher(teacher);
        }

        public void RecordPayment(Student student, decimal amount, DateTime paymentDate)
        {
            student.MakePayment(amount, paymentDate, this);
        }

        public void GenerateEnrollmentReport(Course course)
        {
            Console.WriteLine($"Enrollment Report for {course.CourseName}:");
            foreach (var enrollment in course.Enrollments)
            {
                Console.WriteLine($"- {enrollment.Student.FirstName} {enrollment.Student.LastName} (Enrolled: {enrollment.EnrollmentDate.ToShortDateString()})");
            }
        }

        public void GeneratePaymentReport(Student student)
        {
            Console.WriteLine($"Payment Report for {student.FirstName} {student.LastName}:");
            foreach (var payment in student.Payments)
            {
                Console.WriteLine($"- ${payment.Amount} on {payment.PaymentDate.ToShortDateString()}");
            }
        }

        public void CalculateCourseStatistics(Course course)
        {
            int enrollmentCount = course.Enrollments.Count;
            decimal totalPayments = 0;

            foreach (var enrollment in course.Enrollments)
            {
                foreach (var payment in enrollment.Student.Payments)
                {
                    totalPayments += payment.Amount;
                }
            }

            Console.WriteLine($"Statistics for {course.CourseName}:");
            Console.WriteLine($"- Number of Enrollments: {enrollmentCount}");
            Console.WriteLine($"- Total Payments: ${totalPayments}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SIS sis = new SIS();

            Teacher t1 = new Teacher(1001, "Ram", "CHaran", "ram@university.edu");
            Teacher t2 = new Teacher(1002, "Hari", "Amar", "hari@university.edu");
            sis.Teachers.Add(t1);
            sis.Teachers.Add(t2);

            Course c1 = new Course(101, "Mathematics", "MATH101");
            Course c2 = new Course(102, "Computer Science", "CS102");
            sis.Courses.Add(c1);
            sis.Courses.Add(c2);

            sis.AssignTeacherToCourse(t1, c1);
            sis.AssignTeacherToCourse(t2, c2);

            Student s1 = new Student(1, "Manish", "Sivakumar", new DateTime(2000, 5, 15), "manish@student.edu", "1234567890");
            Student s2 = new Student(2, "Ismail", "Ali", new DateTime(2001, 3, 22), "ismail@student.edu", "2345678901");
            sis.Students.Add(s1);
            sis.Students.Add(s2);

            sis.EnrollStudentInCourse(s1, c1, DateTime.Now);
            sis.EnrollStudentInCourse(s1, c2, DateTime.Now);
            sis.EnrollStudentInCourse(s2, c1, DateTime.Now);

            sis.RecordPayment(s1, 500.00m, DateTime.Now);
            sis.RecordPayment(s1, 250.00m, DateTime.Now.AddDays(-10));
            sis.RecordPayment(s2, 500.00m, DateTime.Now);

            // Display information
            Console.WriteLine("=== Student Information ===");
            s1.DisplayStudentInfo();
            Console.WriteLine("\nEnrolled Courses:");
            foreach (var course in s1.GetEnrolledCourses())
            {
                Console.WriteLine($"- {course.CourseName}");
            }

            Console.WriteLine("\n=== Course Information ===");
            c1.DisplayCourseInfo();

            Console.WriteLine("\n=== Enrollment Report ===");
            sis.GenerateEnrollmentReport(c1);

            Console.WriteLine("\n=== Payment Report ===");
            sis.GeneratePaymentReport(s1);

            Console.WriteLine("\n=== Course Statistics ===");
            sis.CalculateCourseStatistics(c1);
        }
    }
}
