using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;


namespace StudentInformationSystem.dao.Implementations
{
    public class TeacherServiceImpl : ITeacherServiceDao
    {
        public void AddTeacher(Teacher teacher)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkIdQuery = "SELECT COUNT(*) FROM Teachers WHERE TeacherId = @TeacherId";
                SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, con);
                checkIdCmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherID);
                int idCount = (int)checkIdCmd.ExecuteScalar();

                if (idCount > 0)
                {
                    throw new exception.DuplicateIdException($"Teacher with ID {teacher.TeacherID} already exists");
                }

                string checkEmailQuery = "SELECT COUNT(*) FROM Teachers WHERE Email = @Email";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, con);
                checkEmailCmd.Parameters.AddWithValue("@Email", teacher.Email);
                int emailCount = (int)checkEmailCmd.ExecuteScalar();

                if (emailCount > 0)
                {
                    throw new exception.InvalidTeacherDataException("Email already exists");
                }

                string query = @"INSERT INTO Teachers (TeacherId, FirstName, LastName, Email) 
                                VALUES (@TeacherId, @FirstName, @LastName, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM Teachers WHERE TeacherId = @TeacherId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Teacher with ID {teacher.TeacherID} not found");
                }

                checkQuery = "SELECT COUNT(*) FROM Teachers WHERE Email = @Email AND TeacherId != @TeacherId";
                checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@Email", teacher.Email);
                checkCmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherID);
                count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new exception.InvalidTeacherDataException("Email already used by another teacher");
                }

                string query = @"UPDATE Teachers SET FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE TeacherId = @TeacherId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherID);
                cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "SELECT COUNT(*) FROM Teachers WHERE TeacherId = @TeacherId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Teacher with ID {teacherId} not found");
                }

                string checkCoursesQuery = "SELECT COUNT(*) FROM Courses WHERE InstructorId = @TeacherId";
                SqlCommand checkCoursesCmd = new SqlCommand(checkCoursesQuery, con);
                checkCoursesCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                int coursesCount = (int)checkCoursesCmd.ExecuteScalar();

                if (coursesCount > 0)
                {
                    throw new exception.InvalidTeacherDataException("Cannot delete teacher assigned to courses");
                }

                string deleteQuery = "DELETE FROM Teachers WHERE TeacherId = @TeacherId";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                deleteCmd.ExecuteNonQuery();
            }
        }

        public Teacher GetTeacherById(int teacherId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Teachers WHERE TeacherId = @TeacherId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Teacher
                        {
                            TeacherID = (int)reader["TeacherId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                    else
                    {
                        throw new exception.TeacherNotFoundException();
                    }
                }
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Teachers";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teachers.Add(new Teacher
                        {
                            TeacherID = (int)reader["TeacherId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return teachers;
        }

        public List<Course> GetAssignedCourses(int teacherId)
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Courses WHERE InstructorId = @TeacherId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);

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
    }
}