using Task_7_11_SIS.Data;
using Task_7_11_SIS.Models;
using static System.Console;

namespace Task_7_11_SIS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentDaoImpl studentDao = new StudentDaoImpl();
            CourseDaoImpl courseDao = new CourseDaoImpl();
            TeacherDaoImpl teacherDao = new TeacherDaoImpl();
            PaymentDaoImpl paymentDao = new PaymentDaoImpl();
            EnrollmentDaoImpl enrollmentDao = new EnrollmentDaoImpl();
            UserInterface ui = new UserInterface();

            try
            {
                // Task 8: Student Enrollment
                WriteLine("\n=== Student Enrollment ===");
                Student student = new Student
                {
                    StudentId = ui.GetStudentId(),
                    FirstName = ui.GetFirstName(),
                    LastName = ui.GetLastName(),
                    DateOfBirth = ui.GetDateOfBirth(),
                    Email = ui.GetEmail(),
                    PhoneNumber = ui.GetPhoneNumber()
                };

                studentDao.AddStudent(student);
                WriteLine("\nStudent added successfully!");

                // Enroll in courses
                int courseCount = ui.GetCourseCount();
                for (int i = 0; i < courseCount; i++)
                {
                    int courseId = ui.GetCourseId($"Enter Course ID {i + 1}: ");
                    int enrollmentId = ui.GetEnrollmentId();
                    enrollmentDao.EnrollStudentInCourse(student.StudentId, courseId, DateTime.Now, enrollmentId);
                }
                WriteLine("Enrollment completed!");

                // Task 9: Teacher Assignment
                WriteLine("\n=== Teacher Assignment ===");
                int assignCourseId = ui.GetCourseId("Enter Course ID to assign teacher: ");
                int teacherId = ui.GetTeacherId();
                courseDao.AssignTeacherToCourse(assignCourseId, teacherId);
                WriteLine("\nTeacher assigned successfully!");

                // Task 10: Payment Record
                WriteLine("\n=== Record Payment ===");
                Payment payment = new Payment
                {
                    StudentId = ui.GetStudentId(),
                    Amount = ui.GetPaymentAmount(),
                    PaymentDate = DateTime.Now
                };
                int paymentId = ui.GetPaymentId();
                paymentDao.RecordPayment(payment, paymentId);
                WriteLine("\nPayment recorded successfully!");

                // Task 11: Enrollment Report
                WriteLine("\n=== Enrollment Report ===");
                int reportCourseId = ui.GetCourseId();
                var enrollments = enrollmentDao.GetEnrollmentsByCourseId(reportCourseId);

                WriteLine($"\nEnrollments for Course ID {reportCourseId}:");
                foreach (var e in enrollments)
                {
                    WriteLine($"Student ID: {e.Student.StudentId}, Name: {e.Student.FirstName} {e.Student.LastName}");
                }
            }
            catch (Exception ex)
            {
                WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                WriteLine("\nProgram completed. Press any key to exit...");
                ReadKey();
            }
        }
    }
}