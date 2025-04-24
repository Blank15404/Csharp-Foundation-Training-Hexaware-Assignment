using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;


namespace StudentInformationSystem.dao.Implementations
{
    public class StudentServiceImpl : IStudentServiceDao
    {
        public void AddStudent(Student student)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "Select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@StudentId", student.StudentID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new exception.DuplicateIdException($"Student with ID {student.StudentID} already exists");
                }

                checkQuery = "select count(*) from Students where Email = @Email";
                checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@Email", student.Email);
                count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new exception.InvalidStudentDataException("Email already exists");
                }

                string query = @"Insert into Students (StudentId, FirstName, LastName, DateOfBirth, Email, PhoneNumber) 
                                values (@StudentId, @FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", student.StudentID);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@StudentId", student.StudentID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Student with ID {student.StudentID} not found");
                }

                checkQuery = "Select count(*) from Students where Email = @Email and StudentId != @StudentId";
                checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@Email", student.Email);
                checkCmd.Parameters.AddWithValue("@StudentId", student.StudentID);
                count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new exception.InvalidStudentDataException("Email already used by another student");
                }

                string query = @"update Students set FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Email = @Email, PhoneNumber = @PhoneNumber where StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", student.StudentID);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM Students WHERE StudentId = @StudentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@StudentId", studentId);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Student with ID {studentId} not found");
                }

                string deleteEnrollmentsQuery = "DELETE FROM Enrollments WHERE StudentId = @StudentId";
                SqlCommand deleteEnrollmentsCmd = new SqlCommand(deleteEnrollmentsQuery, con);
                deleteEnrollmentsCmd.Parameters.AddWithValue("@StudentId", studentId);
                deleteEnrollmentsCmd.ExecuteNonQuery();

                string deletePaymentsQuery = "DELETE FROM Payments WHERE StudentId = @StudentId";
                SqlCommand deletePaymentsCmd = new SqlCommand(deletePaymentsQuery, con);
                deletePaymentsCmd.Parameters.AddWithValue("@StudentId", studentId);
                deletePaymentsCmd.ExecuteNonQuery();

                string deleteStudentQuery = "DELETE FROM Students WHERE StudentId = @StudentId";
                SqlCommand deleteStudentCmd = new SqlCommand(deleteStudentQuery, con);
                deleteStudentCmd.Parameters.AddWithValue("@StudentId", studentId);
                deleteStudentCmd.ExecuteNonQuery();
            }
        }

        public Student GetStudentById(int studentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Students WHERE StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = (int)reader["StudentId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : null
                        };
                    }
                    else
                    {
                        throw new exception.StudentNotFoundException();
                    }
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = (int)reader["StudentId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader["PhoneNumber"].ToString() : null
                        });
                    }
                }
            }
            return students;
        }

        public void EnrollStudentInCourse(int studentId, int courseId, DateTime enrollmentDate)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkStudentQuery = "SELECT COUNT(*) FROM Students WHERE StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", studentId);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string checkCourseQuery = "SELECT COUNT(*) FROM Courses WHERE CourseId = @CourseId";
                SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, con);
                checkCourseCmd.Parameters.AddWithValue("@CourseId", courseId);
                int courseCount = (int)checkCourseCmd.ExecuteScalar();

                if (courseCount == 0)
                {
                    throw new exception.CourseNotFoundException();
                }

                string checkEnrollmentQuery = "SELECT COUNT(*) FROM Enrollments WHERE StudentId = @StudentId AND CourseId = @CourseId";
                SqlCommand checkEnrollmentCmd = new SqlCommand(checkEnrollmentQuery, con);
                checkEnrollmentCmd.Parameters.AddWithValue("@StudentId", studentId);
                checkEnrollmentCmd.Parameters.AddWithValue("@CourseId", courseId);
                int enrollmentCount = (int)checkEnrollmentCmd.ExecuteScalar();

                if (enrollmentCount > 0)
                {
                    throw new exception.DuplicateEnrollmentException();
                }

                string getNextIdQuery = "SELECT ISNULL(MAX(EnrollmentId), 0) + 1 FROM Enrollments";
                SqlCommand getNextIdCmd = new SqlCommand(getNextIdQuery, con);
                int nextEnrollmentId = (int)getNextIdCmd.ExecuteScalar();

                string insertQuery = @"INSERT INTO Enrollments (EnrollmentId, StudentId, CourseId, EnrollmentDate)
                                       VALUES (@EnrollmentId, @StudentId, @CourseId, @EnrollmentDate)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@EnrollmentId", nextEnrollmentId);
                insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                insertCmd.Parameters.AddWithValue("@CourseId", courseId);
                insertCmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);

                insertCmd.ExecuteNonQuery();
            }
        }

        public List<Course> GetEnrolledCourses(int studentId)
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = @"SELECT c.CourseId, c.CourseName, c.CourseCode, c.InstructorId 
                                 FROM Courses c
                                 JOIN Enrollments e ON c.CourseId = e.CourseId
                                 WHERE e.StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = (int)reader["CourseId"],
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            InstructorId = (int)reader["InstructorId"]
                        });
                    }
                }
            }
            return courses;
        }

        public void MakePayment(int studentId, decimal amount, DateTime paymentDate)
        {
            if (amount <= 0)
            {
                throw new exception.PaymentValidationException();
            }

            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkStudentQuery = "SELECT COUNT(*) FROM Students WHERE StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", studentId);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string getNextIdQuery = "SELECT ISNULL(MAX(PaymentId), 0) + 1 FROM Payments";
                SqlCommand getNextIdCmd = new SqlCommand(getNextIdQuery, con);
                int nextPaymentId = (int)getNextIdCmd.ExecuteScalar();

                string insertQuery = @"INSERT INTO Payments (PaymentId, StudentId, Amount, PaymentDate)
                                       VALUES (@PaymentId, @StudentId, @Amount, @PaymentDate)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@PaymentId", nextPaymentId);
                insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                insertCmd.Parameters.AddWithValue("@Amount", amount);
                insertCmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                insertCmd.ExecuteNonQuery();
            }
        }

        public List<Payment> GetPaymentHistory(int studentId)
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Payments WHERE StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            PaymentID = (int)reader["PaymentId"],
                            StudentID = (int)reader["StudentId"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        });
                    }
                }
            }
            return payments;
        }
    }
}