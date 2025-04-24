using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;


namespace StudentInformationSystem.dao.Implementations
{
    public class EnrollmentServiceImpl : IEnrollmentServiceDao
    {
        public void AddEnrollment(Enrollment enrollment)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkIdQuery = "select count(*) from Enrollments where EnrollmentId = @EnrollmentId";
                SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, con);
                checkIdCmd.Parameters.AddWithValue("@EnrollmentId", enrollment.EnrollmentID);
                int idCount = (int)checkIdCmd.ExecuteScalar();

                if (idCount > 0)
                {
                    throw new exception.DuplicateIdException($"Enrollment with ID {enrollment.EnrollmentID} already exists");
                }

                string checkStudentQuery = "select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", enrollment.StudentID);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string checkCourseQuery = "select count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, con);
                checkCourseCmd.Parameters.AddWithValue("@CourseId", enrollment.CourseID);
                int courseCount = (int)checkCourseCmd.ExecuteScalar();

                if (courseCount == 0)
                {
                    throw new exception.CourseNotFoundException();
                }

                string checkEnrollmentQuery = "Select count(*) from Enrollments where StudentId = @StudentId and CourseId = @CourseId";
                SqlCommand checkEnrollmentCmd = new SqlCommand(checkEnrollmentQuery, con);
                checkEnrollmentCmd.Parameters.AddWithValue("@StudentId", enrollment.StudentID);
                checkEnrollmentCmd.Parameters.AddWithValue("@CourseId", enrollment.CourseID);
                int enrollmentCount = (int)checkEnrollmentCmd.ExecuteScalar();

                if (enrollmentCount > 0)
                {
                    throw new exception.DuplicateEnrollmentException();
                }

                string query = @"Insert into Enrollments (EnrollmentId, StudentId, CourseId, EnrollmentDate) 
                                values (@EnrollmentId, @StudentId, @CourseId, @EnrollmentDate)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollment.EnrollmentID);
                cmd.Parameters.AddWithValue("@StudentId", enrollment.StudentID);
                cmd.Parameters.AddWithValue("@CourseId", enrollment.CourseID);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollment.EnrollmentDate);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "select count(*) from Enrollments where EnrollmentId = @EnrollmentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@EnrollmentId", enrollment.EnrollmentID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Enrollment with ID {enrollment.EnrollmentID} not found");
                }

                string checkStudentQuery = "select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", enrollment.StudentID);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string checkCourseQuery = "select count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, con);
                checkCourseCmd.Parameters.AddWithValue("@CourseId", enrollment.CourseID);
                int courseCount = (int)checkCourseCmd.ExecuteScalar();

                if (courseCount == 0)
                {
                    throw new exception.CourseNotFoundException();
                }


                string query = @"Update Enrollments set StudentId = @StudentId, CourseId = @CourseId, EnrollmentDate = @EnrollmentDate where EnrollmentId = @EnrollmentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollment.EnrollmentID);
                cmd.Parameters.AddWithValue("@StudentId", enrollment.StudentID);
                cmd.Parameters.AddWithValue("@CourseId", enrollment.CourseID);
                cmd.Parameters.AddWithValue("@EnrollmentDate", enrollment.EnrollmentDate);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "Select count(*) from Enrollments where EnrollmentId = @EnrollmentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Enrollment with ID {enrollmentId} not found");
                }

                string deleteQuery = "delete from Enrollments where EnrollmentId = @EnrollmentId";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);
                deleteCmd.ExecuteNonQuery();
            }
        }

        public Enrollment GetEnrollmentById(int enrollmentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "Select * from Enrollments where EnrollmentId = @EnrollmentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Enrollment
                        {
                            EnrollmentID = (int)reader["EnrollmentId"],
                            StudentID = (int)reader["StudentId"],
                            CourseID = (int)reader["CourseId"],
                            EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                        };
                    }
                    else
                    {
                        throw new exception.IdNotFoundException($"Enrollment with ID {enrollmentId} not found");
                    }
                }
            }
        }

        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "select * from Enrollments";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        enrollments.Add(new Enrollment
                        {
                            EnrollmentID = (int)reader["EnrollmentId"],
                            StudentID = (int)reader["StudentId"],
                            CourseID = (int)reader["CourseId"],
                            EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                        });
                    }
                }
            }
            return enrollments;
        }
    }
}