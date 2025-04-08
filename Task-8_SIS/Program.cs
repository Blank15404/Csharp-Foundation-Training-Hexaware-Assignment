using System;

namespace Task_8_SIS
{
    class Program
    {
        static void Main(string[] args)
        {
            SIS sis = new SIS();

            while (true)
            {
                Console.WriteLine("\n--- Student Information System ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Add Teacher");
                Console.WriteLine("4. Enroll Student");
                Console.WriteLine("5. Assign Teacher to Course");
                Console.WriteLine("6. Record Payment");
                Console.WriteLine("7. View Student Enrollments");
                Console.WriteLine("8. View Teacher Courses");
                Console.WriteLine("9. View All Data");
                Console.WriteLine("10. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": // Add Student
                            Console.Write("Enter Student ID: ");
                            int studentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                            DateTime dob = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone: ");
                            string phone = Console.ReadLine();

                            sis.AddStudent(studentId, firstName, lastName, dob, email, phone);
                            Console.WriteLine("Student added successfully!");
                            break;

                        case "2": // Add Course
                            Console.Write("Enter Course ID: ");
                            int courseId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Course Name: ");
                            string courseName = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            string courseCode = Console.ReadLine();

                            sis.AddCourse(courseId, courseName, courseCode);
                            Console.WriteLine("Course added successfully!");
                            break;

                        case "3": // Add Teacher
                            Console.Write("Enter Teacher ID: ");
                            int teacherId = int.Parse(Console.ReadLine());
                            Console.Write("Enter First Name: ");
                            string tFirstName = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string tLastName = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string tEmail = Console.ReadLine();

                            sis.AddTeacher(teacherId, tFirstName, tLastName, tEmail);
                            Console.WriteLine("Teacher added successfully!");
                            break;

                        case "4": // Enroll Student
                            Console.Write("Enter Student ID: ");
                            int enrollStudentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Course ID: ");
                            int enrollCourseId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Enrollment Date (yyyy-mm-dd): ");
                            DateTime enrollDate = DateTime.Parse(Console.ReadLine());

                            sis.EnrollStudent(enrollStudentId, enrollCourseId, enrollDate);
                            Console.WriteLine("Student enrolled successfully!");
                            break;

                        case "5": // Assign Teacher to Course
                            Console.Write("Enter Teacher ID: ");
                            int assignTeacherId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Course ID: ");
                            int assignCourseId = int.Parse(Console.ReadLine());

                            sis.AssignTeacherToCourse(assignTeacherId, assignCourseId);
                            Console.WriteLine("Teacher assigned to course successfully!");
                            break;

                        case "6": // Record Payment
                            Console.Write("Enter Student ID: ");
                            int paymentStudentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter Payment Date (yyyy-mm-dd): ");
                            DateTime paymentDate = DateTime.Parse(Console.ReadLine());

                            sis.AddPayment(paymentStudentId, amount, paymentDate);
                            Console.WriteLine("Payment recorded successfully!");
                            break;

                        case "7": // View Student Enrollments
                            Console.WriteLine("\nAvailable Students:");
                            foreach (var student in sis.Students)
                            {
                                Console.WriteLine($"{student.StudentID}: {student.FirstName} {student.LastName}");
                            }
                            Console.Write("\nEnter Student ID: ");
                            int viewStudentId = int.Parse(Console.ReadLine());
                            var enrollments = sis.GetEnrollmentsForStudent(viewStudentId);
                            Console.WriteLine($"\nEnrollments for Student ID {viewStudentId}:");
                            foreach (var e in enrollments)
                            {
                                Console.WriteLine($"- {e.Course.CourseName} (Enrolled: {e.EnrollmentDate:d})");
                            }
                            break;

                        case "8": // View Teacher Courses
                            Console.WriteLine("\nAvailable Teachers:");
                            foreach (var teacher in sis.Teachers)
                            {
                                Console.WriteLine($"{teacher.TeacherID}: {teacher.FirstName} {teacher.LastName}");
                            }
                            Console.Write("\nEnter Teacher ID: ");
                            int viewTeacherId = int.Parse(Console.ReadLine());
                            var courses = sis.GetCoursesForTeacher(viewTeacherId);
                            Console.WriteLine($"\nCourses for Teacher ID {viewTeacherId}:");
                            foreach (var c in courses)
                            {
                                Console.WriteLine($"- {c.CourseName}");
                            }
                            break;

                        case "9": // View All Data
                            Console.WriteLine("\n=== ALL DATA ===");

                            Console.WriteLine("\nSTUDENTS:");
                            foreach (var s in sis.Students)
                            {
                                Console.WriteLine($"{s.StudentID}: {s.FirstName} {s.LastName}");
                                Console.WriteLine("  Enrollments:");
                                foreach (var e in s.Enrollments)
                                {
                                    Console.WriteLine($"  - {e.Course.CourseName}");
                                }
                                Console.WriteLine("  Payments:");
                                foreach (var p in s.Payments)
                                {
                                    Console.WriteLine($"  - ${p.Amount} on {p.PaymentDate:d}");
                                }
                            }

                            Console.WriteLine("\nCOURSES:");
                            foreach (var c in sis.Courses)
                            {
                                Console.WriteLine($"{c.CourseID}: {c.CourseName}");
                                Console.WriteLine($"  Instructor: {c.Instructor?.FirstName} {c.Instructor?.LastName}");
                                Console.WriteLine("  Enrolled Students:");
                                foreach (var e in c.Enrollments)
                                {
                                    Console.WriteLine($"  - {e.Student.FirstName} {e.Student.LastName}");
                                }
                            }

                            Console.WriteLine("\nTEACHERS:");
                            foreach (var t in sis.Teachers)
                            {
                                Console.WriteLine($"{t.TeacherID}: {t.FirstName} {t.LastName}");
                                Console.WriteLine("  Assigned Courses:");
                                foreach (var c in t.AssignedCourses)
                                {
                                    Console.WriteLine($"  - {c.CourseName}");
                                }
                            }
                            break;

                        case "10": // Exit
                            Console.WriteLine("Exiting program...");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
