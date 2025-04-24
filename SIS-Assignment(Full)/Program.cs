using System;
using StudentInformationSystem.dao.Implementations;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;


namespace StudentInformationSystem
{
    class Program
    {
        private static IStudentServiceDao studentDao = new StudentServiceImpl();
        private static ICourseServiceDao courseDao = new CourseServiceImpl();
        private static ITeacherServiceDao teacherDao = new TeacherServiceImpl();
        private static IEnrollmentServiceDao enrollmentDao = new EnrollmentServiceImpl();
        private static IPaymentServiceDao paymentDao = new PaymentServiceImpl();
        private static UserInterface ui = new UserInterface();

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    int choice = ui.GetMainMenuChoice();

                    switch (choice)
                    {
                        case 1:
                            StudentMenu();
                            break;
                        case 2:
                            CourseMenu();
                            break;
                        case 3:
                            TeacherMenu();
                            break;
                        case 4:
                            EnrollmentMenu();
                            break;
                        case 5:
                            PaymentMenu();
                            break;
                        case 6:
                            return;
                        default:
                            ui.ShowMessage("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void StudentMenu()
        {
            while (true)
            {
                int choice = ui.GetStudentMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        UpdateStudent();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:
                        ViewStudentById();
                        break;
                    case 5:
                        ViewAllStudents();
                        break;
                    case 6:
                        EnrollStudentInCourse();
                        break;
                    case 7:
                        MakePayment();
                        break;
                    case 8:
                        ViewPaymentHistory();
                        break;
                    case 9:
                        ViewEnrolledCourses();
                        break;
                    case 10:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            try
            {
                Student student = ui.GetStudentDetails();
                studentDao.AddStudent(student);
                ui.ShowMessage("Student added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void UpdateStudent()
        {
            try
            {
                Student student = ui.GetStudentDetails();
                studentDao.UpdateStudent(student);
                ui.ShowMessage("Student updated successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void DeleteStudent()
        {
            try
            {
                int studentId = ui.GetStudentId();
                studentDao.DeleteStudent(studentId);
                ui.ShowMessage("Student deleted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewStudentById()
        {
            try
            {
                int studentId = ui.GetStudentId();
                Student student = studentDao.GetStudentById(studentId);
                ui.DisplayStudentDetails(student);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllStudents()
        {
            try
            {
                var students = studentDao.GetAllStudents();
                ui.DisplayStudents(students);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void EnrollStudentInCourse()
        {
            try
            {
                var enrollmentData = ui.GetEnrollmentTuple();
                studentDao.EnrollStudentInCourse(enrollmentData.studentId, enrollmentData.courseId, enrollmentData.date);
                ui.ShowMessage("Student enrolled in course successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void MakePayment()
        {
            try
            {
                var paymentData = ui.GetPaymentTuple();
                studentDao.MakePayment(paymentData.studentId, paymentData.amount, paymentData.date);
                ui.ShowMessage("Payment recorded successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewPaymentHistory()
        {
            try
            {
                int studentId = ui.GetStudentId();
                var payments = studentDao.GetPaymentHistory(studentId);
                ui.DisplayPayments(payments);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewEnrolledCourses()
        {
            try
            {
                int studentId = ui.GetStudentId();
                var courses = studentDao.GetEnrolledCourses(studentId);
                ui.DisplayCourses(courses);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void CourseMenu()
        {
            while (true)
            {
                int choice = ui.GetCourseMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddCourse();
                        break;
                    case 2:
                        UpdateCourse();
                        break;
                    case 3:
                        DeleteCourse();
                        break;
                    case 4:
                        ViewCourseById();
                        break;
                    case 5:
                        ViewAllCourses();
                        break;
                    case 6:
                        AssignTeacherToCourse();
                        break;
                    case 7:
                        ViewEnrolledStudents();
                        break;
                    case 8:
                        ViewCourseTeacher();
                        break;
                    case 9:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddCourse()
        {
            try
            {
                Course course = ui.GetCourseDetails();
                courseDao.AddCourse(course);
                ui.ShowMessage("Course added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void UpdateCourse()
        {
            try
            {
                Course course = ui.GetCourseDetails();
                courseDao.UpdateCourse(course);
                ui.ShowMessage("Course updated successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void DeleteCourse()
        {
            try
            {
                int courseId = ui.GetCourseId();
                courseDao.DeleteCourse(courseId);
                ui.ShowMessage("Course deleted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewCourseById()
        {
            try
            {
                int courseId = ui.GetCourseId();
                Course course = courseDao.GetCourseById(courseId);
                ui.DisplayCourseDetails(course);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllCourses()
        {
            try
            {
                var courses = courseDao.GetAllCourses();
                ui.DisplayCourses(courses);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void AssignTeacherToCourse()
        {
            try
            {
                var assignmentData = ui.GetTeacherAssignmentDetails();
                courseDao.AssignTeacherToCourse(assignmentData.courseId, assignmentData.teacherId);
                ui.ShowMessage("Teacher assigned to course successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewEnrolledStudents()
        {
            try
            {
                int courseId = ui.GetCourseId();
                var students = courseDao.GetEnrolledStudents(courseId);
                ui.DisplayStudents(students);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewCourseTeacher()
        {
            try
            {
                int courseId = ui.GetCourseId();
                Teacher teacher = courseDao.GetCourseTeacher(courseId);
                ui.DisplayTeacherDetails(teacher);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void TeacherMenu()
        {
            while (true)
            {
                int choice = ui.GetTeacherMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddTeacher();
                        break;
                    case 2:
                        UpdateTeacher();
                        break;
                    case 3:
                        DeleteTeacher();
                        break;
                    case 4:
                        ViewTeacherById();
                        break;
                    case 5:
                        ViewAllTeachers();
                        break;
                    case 6:
                        ViewAssignedCourses();
                        break;
                    case 7:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddTeacher()
        {
            try
            {
                Teacher teacher = ui.GetTeacherDetails();
                teacherDao.AddTeacher(teacher);
                ui.ShowMessage("Teacher added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void UpdateTeacher()
        {
            try
            {
                Teacher teacher = ui.GetTeacherDetails();
                teacherDao.UpdateTeacher(teacher);
                ui.ShowMessage("Teacher updated successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void DeleteTeacher()
        {
            try
            {
                int teacherId = ui.GetTeacherId();
                teacherDao.DeleteTeacher(teacherId);
                ui.ShowMessage("Teacher deleted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewTeacherById()
        {
            try
            {
                int teacherId = ui.GetTeacherId();
                Teacher teacher = teacherDao.GetTeacherById(teacherId);
                ui.DisplayTeacherDetails(teacher);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllTeachers()
        {
            try
            {
                var teachers = teacherDao.GetAllTeachers();
                ui.DisplayTeachers(teachers);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAssignedCourses()
        {
            try
            {
                int teacherId = ui.GetTeacherId();
                var courses = teacherDao.GetAssignedCourses(teacherId);
                ui.DisplayCourses(courses);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void EnrollmentMenu()
        {
            while (true)
            {
                int choice = ui.GetEnrollmentMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddEnrollment();
                        break;
                    case 2:
                        UpdateEnrollment();
                        break;
                    case 3:
                        DeleteEnrollment();
                        break;
                    case 4:
                        ViewEnrollmentById();
                        break;
                    case 5:
                        ViewAllEnrollments();
                        break;
                    case 6:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddEnrollment()
        {
            try
            {
                Enrollment enrollment = ui.GetEnrollmentObject();
                enrollmentDao.AddEnrollment(enrollment);
                ui.ShowMessage("Enrollment added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void UpdateEnrollment()
        {
            try
            {
                Enrollment enrollment = ui.GetEnrollmentObject();
                enrollmentDao.UpdateEnrollment(enrollment);
                ui.ShowMessage("Enrollment updated successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void DeleteEnrollment()
        {
            try
            {
                int enrollmentId = ui.GetEnrollmentId();
                enrollmentDao.DeleteEnrollment(enrollmentId);
                ui.ShowMessage("Enrollment deleted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewEnrollmentById()
        {
            try
            {
                int enrollmentId = ui.GetEnrollmentId();
                Enrollment enrollment = enrollmentDao.GetEnrollmentById(enrollmentId);
                ui.DisplayEnrollmentDetails(enrollment);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllEnrollments()
        {
            try
            {
                var enrollments = enrollmentDao.GetAllEnrollments();
                ui.DisplayEnrollments(enrollments);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void PaymentMenu()
        {
            while (true)
            {
                int choice = ui.GetPaymentMenuChoice();

                switch (choice)
                {
                    case 1:
                        AddPayment();
                        break;
                    case 2:
                        UpdatePayment();
                        break;
                    case 3:
                        DeletePayment();
                        break;
                    case 4:
                        ViewPaymentById();
                        break;
                    case 5:
                        ViewAllPayments();
                        break;
                    case 6:
                        ViewPaymentsByStudent();
                        break;
                    case 7:
                        return;
                    default:
                        ui.ShowMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddPayment()
        {
            try
            {
                Payment payment = ui.GetPaymentObject();
                paymentDao.AddPayment(payment);
                ui.ShowMessage("Payment added successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void UpdatePayment()
        {
            try
            {
                Payment payment = ui.GetPaymentObject();
                paymentDao.UpdatePayment(payment);
                ui.ShowMessage("Payment updated successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void DeletePayment()
        {
            try
            {
                int paymentId = ui.GetPaymentId();
                paymentDao.DeletePayment(paymentId);
                ui.ShowMessage("Payment deleted successfully!");
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewPaymentById()
        {
            try
            {
                int paymentId = ui.GetPaymentId();
                Payment payment = paymentDao.GetPaymentById(paymentId);
                ui.DisplayPaymentDetails(payment);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewAllPayments()
        {
            try
            {
                var payments = paymentDao.GetAllPayments();
                ui.DisplayPayments(payments);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }

        static void ViewPaymentsByStudent()
        {
            try
            {
                int studentId = ui.GetStudentId();
                var payments = paymentDao.GetPaymentsByStudent(studentId);
                ui.DisplayPayments(payments);
            }
            catch (Exception ex)
            {
                ui.ShowError(ex.Message);
            }
        }
    }
}