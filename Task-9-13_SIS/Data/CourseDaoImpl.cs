using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal class CourseDaoImpl : ICourseDao
    {
        public int AddCourse(Course course)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Courses (CourseId, CourseName, CourseCode, InstructorId) values (@CourseId, @CourseName, @CourseCode, @InstructorId)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                    cmd.Parameters.AddWithValue("@InstructorId", (object)course.InstructorId ?? DBNull.Value);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Course could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding a course.", ex);
            }

            return rowsAffected;
        }

        public Course GetCourseById(int id)
        {
            Course course = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Courses where CourseId = @CourseId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            course = new Course
                            {
                                CourseId = (int)reader["CourseId"],
                                CourseName = (string)reader["CourseName"],
                                CourseCode = (string)reader["CourseCode"],
                                InstructorId = reader["InstructorId"] as int? ?? null
                            };
                        }
                    }
                }

                if (course == null)
                {
                    throw new SISException($"Course with ID {id} not found.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching course with the given ID.", ex);
            }

            return course;
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Courses";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CourseId = (int)reader["CourseId"],
                                CourseName = (string)reader["CourseName"],
                                CourseCode = (string)reader["CourseCode"],
                                InstructorId = reader["InstructorId"] as int? ?? null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all courses.", ex);
            }

            return courses;
        }

        public int AssignTeacherToCourse(int courseId, int teacherId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = "update Courses set InstructorId = @InstructorId where CourseId = @CourseId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.Parameters.AddWithValue("@InstructorId", teacherId);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("No course found with the given ID to update.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error assigning teacher to course.", ex);
            }

            return rowsAffected;
        }

    }
}
