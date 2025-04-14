using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;
using static Task_7_11_SIS.Models.Student;

namespace Task_7_11_SIS.Data
{
    internal class EnrollmentDaoImpl : IEnrollmentDao
    {
        public int EnrollStudentInCourse(int studentId, int courseId, DateTime enrollmentDate, int enrollmentId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            // First check if the student is already enrolled in the course
            string checkQuery = "select count(*) from Enrollments where StudentId = @StudentId and CourseId = @CourseId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    // Check for existing enrollment
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount > 0)
                    {
                        throw new SISException($"Student {studentId} is already enrolled in course {courseId}.");
                    }

                    // If not enrolled, proceed with enrollment
                    string insertQuery = @"INSERT INTO Enrollments (EnrollmentId, StudentId, CourseId, EnrollmentDate) 
                                          VALUES (@EnrollmentId, @StudentId, @CourseId, @EnrollmentDate)";

                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@EnrollmentDate", enrollmentDate);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Enrollment could not be completed.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in enrolling student in course.", ex);
            }

            return rowsAffected;
        }

        public List<Enrollment> GetEnrollmentsByCourseId(int courseId)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = @"select e.EnrollmentId, e.EnrollmentDate, s.StudentId, s.FirstName, s.LastName, s.DateOfBirth, s.Email, s.PhoneNumber
                           from Enrollments e inner join Students s on e.StudentId = s.StudentId where e.CourseId = @CourseId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Enrollment enrollment = new Enrollment
                            {
                                EnrollmentId = (int)reader["EnrollmentId"],
                                EnrollmentDate = (DateTime)reader["EnrollmentDate"],
                                Student = new Student
                                {
                                    StudentId = (int)reader["StudentId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    DateOfBirth = (DateTime)reader["DateOfBirth"],
                                    Email = (string)reader["Email"],
                                    PhoneNumber = reader["PhoneNumber"] as string
                                }
                            };
                            enrollments.Add(enrollment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving enrollments for course.", ex);
            }

            return enrollments;
        }
    }
}
