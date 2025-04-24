using System;
using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem
{
    class UserInterface
    {
        public int GetMainMenuChoice()
        {
            Console.WriteLine("\nStudent Information System (SIS)");
            Console.WriteLine("1. Student Management");
            Console.WriteLine("2. Course Management");
            Console.WriteLine("3. Teacher Management");
            Console.WriteLine("4. Enrollment Management");
            Console.WriteLine("5. Payment Management");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetStudentMenuChoice()
        {
            Console.WriteLine("\nStudent Management");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student");
            Console.WriteLine("3. Delete Student");
            Console.WriteLine("4. View Student by ID");
            Console.WriteLine("5. View All Students");
            Console.WriteLine("6. Enroll Student in Course");
            Console.WriteLine("7. Make Payment");
            Console.WriteLine("8. View Payment History");
            Console.WriteLine("9. View Enrolled Courses");
            Console.WriteLine("10. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Student GetStudentDetails()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Date of Birth (YYYY-MM-DD): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone Number (optional): ");
            string phoneNumber = Console.ReadLine();

            return new Student(studentId, firstName, lastName, dateOfBirth, email, phoneNumber);
        }

        public int GetStudentId()
        {
            Console.Write("Enter Student ID: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayStudentDetails(Student student)
        {
            Console.WriteLine($"\nStudent ID: {student.StudentID}");
            Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
            Console.WriteLine($"Date of Birth: {student.DateOfBirth:yyyy-MM-dd}");
            Console.WriteLine($"Email: {student.Email}");
            Console.WriteLine($"Phone: {student.PhoneNumber ?? "N/A"}");
        }

        public void DisplayStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                DisplayStudentDetails(student);
                Console.WriteLine("---------------------");
            }
        }

        public (int studentId, int courseId, DateTime date) GetEnrollmentTuple()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());
            Console.Write("Enter Enrollment Date (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            return (studentId, courseId, date);
        }

        public (int studentId, decimal amount, DateTime date) GetPaymentTuple()
        {
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Payment Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Payment Date (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            return (studentId, amount, date);
        }

        public void DisplayPayments(List<Payment> payments)
        {
            foreach (var payment in payments)
            {
                Console.WriteLine($"\nPayment ID: {payment.PaymentID}");
                Console.WriteLine($"Student ID: {payment.StudentID}");
                Console.WriteLine($"Amount: {payment.Amount:C}");
                Console.WriteLine($"Date: {payment.PaymentDate:yyyy-MM-dd}");
                Console.WriteLine("---------------------");
            }
        }

        public void DisplayCourses(List<Course> courses)
        {
            foreach (var course in courses)
            {
                DisplayCourseDetails(course);
                Console.WriteLine("---------------------");
            }
        }

        public void DisplayCourseDetails(Course course)
        {
            Console.WriteLine($"\nCourse ID: {course.CourseID}");
            Console.WriteLine($"Name: {course.CourseName}");
            Console.WriteLine($"Code: {course.CourseCode}");
            Console.WriteLine($"Instructor ID: {course.InstructorId}");
        }

        public int GetCourseMenuChoice()
        {
            Console.WriteLine("\nCourse Management");
            Console.WriteLine("1. Add Course");
            Console.WriteLine("2. Update Course");
            Console.WriteLine("3. Delete Course");
            Console.WriteLine("4. View Course by ID");
            Console.WriteLine("5. View All Courses");
            Console.WriteLine("6. Assign Teacher to Course");
            Console.WriteLine("7. View Enrolled Students");
            Console.WriteLine("8. View Course Teacher");
            Console.WriteLine("9. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Course GetCourseDetails()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();
            Console.Write("Enter Course Code: ");
            string courseCode = Console.ReadLine();
            Console.Write("Enter Instructor ID (0 if none): ");
            int instructorId = int.Parse(Console.ReadLine());

            return new Course(courseId, courseName, courseCode, instructorId);
        }

        public int GetCourseId()
        {
            Console.Write("Enter Course ID: ");
            return int.Parse(Console.ReadLine());
        }

        public (int courseId, int teacherId) GetTeacherAssignmentDetails()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());
            Console.Write("Enter Teacher ID: ");
            int teacherId = int.Parse(Console.ReadLine());
            return (courseId, teacherId);
        }

        public int GetTeacherMenuChoice()
        {
            Console.WriteLine("\nTeacher Management");
            Console.WriteLine("1. Add Teacher");
            Console.WriteLine("2. Update Teacher");
            Console.WriteLine("3. Delete Teacher");
            Console.WriteLine("4. View Teacher by ID");
            Console.WriteLine("5. View All Teachers");
            Console.WriteLine("6. View Assigned Courses");
            Console.WriteLine("7. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Teacher GetTeacherDetails()
        {
            Console.Write("Enter Teacher ID: ");
            int teacherId = int.Parse(Console.ReadLine());
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            return new Teacher(teacherId, firstName, lastName, email);
        }

        public int GetTeacherId()
        {
            Console.Write("Enter Teacher ID: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayTeacherDetails(Teacher teacher)
        {
            Console.WriteLine($"\nTeacher ID: {teacher.TeacherID}");
            Console.WriteLine($"Name: {teacher.FirstName} {teacher.LastName}");
            Console.WriteLine($"Email: {teacher.Email}");
        }

        public void DisplayTeachers(List<Teacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                DisplayTeacherDetails(teacher);
                Console.WriteLine("---------------------");
            }
        }

        public int GetEnrollmentMenuChoice()
        {
            Console.WriteLine("\nEnrollment Management");
            Console.WriteLine("1. Add Enrollment");
            Console.WriteLine("2. Update Enrollment");
            Console.WriteLine("3. Delete Enrollment");
            Console.WriteLine("4. View Enrollment by ID");
            Console.WriteLine("5. View All Enrollments");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Enrollment GetEnrollmentObject()
        {
            Console.Write("Enter Enrollment ID: ");
            int enrollmentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());
            Console.Write("Enter Enrollment Date (YYYY-MM-DD): ");
            DateTime enrollmentDate = DateTime.Parse(Console.ReadLine());

            return new Enrollment(enrollmentId, studentId, courseId, enrollmentDate);
        }

        public int GetEnrollmentId()
        {
            Console.Write("Enter Enrollment ID: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayEnrollmentDetails(Enrollment enrollment)
        {
            Console.WriteLine($"\nEnrollment ID: {enrollment.EnrollmentID}");
            Console.WriteLine($"Student ID: {enrollment.StudentID}");
            Console.WriteLine($"Course ID: {enrollment.CourseID}");
            Console.WriteLine($"Enrollment Date: {enrollment.EnrollmentDate:yyyy-MM-dd}");
        }

        public void DisplayEnrollments(List<Enrollment> enrollments)
        {
            foreach (var enrollment in enrollments)
            {
                DisplayEnrollmentDetails(enrollment);
                Console.WriteLine("---------------------");
            }
        }

        public int GetPaymentMenuChoice()
        {
            Console.WriteLine("\nPayment Management");
            Console.WriteLine("1. Add Payment");
            Console.WriteLine("2. Update Payment");
            Console.WriteLine("3. Delete Payment");
            Console.WriteLine("4. View Payment by ID");
            Console.WriteLine("5. View All Payments");
            Console.WriteLine("6. View Payments by Student");
            Console.WriteLine("7. Back to Main Menu");
            Console.Write("Enter your choice: ");
            return int.Parse(Console.ReadLine());
        }

        public Payment GetPaymentObject()
        {
            Console.Write("Enter Payment ID: ");
            int paymentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Student ID: ");
            int studentId = int.Parse(Console.ReadLine());
            Console.Write("Enter Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Payment Date (YYYY-MM-DD): ");
            DateTime paymentDate = DateTime.Parse(Console.ReadLine());

            return new Payment(paymentId, studentId, amount, paymentDate);
        }

        public int GetPaymentId()
        {
            Console.Write("Enter Payment ID: ");
            return int.Parse(Console.ReadLine());
        }

        public void DisplayPaymentDetails(Payment payment)
        {
            Console.WriteLine($"\nPayment ID: {payment.PaymentID}");
            Console.WriteLine($"Student ID: {payment.StudentID}");
            Console.WriteLine($"Amount: {payment.Amount:C}");
            Console.WriteLine($"Date: {payment.PaymentDate:yyyy-MM-dd}");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string error)
        {
            Console.WriteLine($"Error: {error}");
        }
    }
}