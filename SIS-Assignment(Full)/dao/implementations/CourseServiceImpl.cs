using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;


namespace StudentInformationSystem.dao.Implementations
{
    public class CourseServiceImpl : ICourseServiceDao
    {
        public void AddCourse(Course course)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkIdQuery = "select count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, con);
                checkIdCmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                int idCount = (int)checkIdCmd.ExecuteScalar();

                if (idCount > 0)
                {
                    throw new exception.DuplicateIdException($"Course with ID {course.CourseID} already exists");
                }

                string checkCodeQuery = "select count(*) from Courses where CourseCode = @CourseCode";
                SqlCommand checkCodeCmd = new SqlCommand(checkCodeQuery, con);
                checkCodeCmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                int codeCount = (int)checkCodeCmd.ExecuteScalar();

                if (codeCount > 0)
                {
                    throw new exception.InvalidCourseDataException("Course code already exists");
                }

                if (course.InstructorId > 0)
                {
                    string checkTeacherQuery = "select count(*) from Teachers where TeacherId = @TeacherId";
                    SqlCommand checkTeacherCmd = new SqlCommand(checkTeacherQuery, con);
                    checkTeacherCmd.Parameters.AddWithValue("@TeacherId", course.InstructorId);
                    int teacherCount = (int)checkTeacherCmd.ExecuteScalar();

                    if (teacherCount == 0)
                    {
                        throw new exception.TeacherNotFoundException();
                    }
                }

                string query = @"insert into Courses (CourseId, CourseName, CourseCode, InstructorId) 
                                values (@CourseId, @CourseName, @CourseCode, @InstructorId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.Parameters.AddWithValue("@InstructorId", course.InstructorId > 0 ? (object)course.InstructorId : DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "Select count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Course with ID {course.CourseID} not found");
                }

                checkQuery = "select count(*) from Courses where CourseCode = @CourseCode and CourseId != @CourseId";
                checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                checkCmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    throw new exception.InvalidCourseDataException("Course code already used by another course");
                }

                if (course.InstructorId > 0)
                {
                    string checkTeacherQuery = "Select count(*) from Teachers where TeacherId = @TeacherId";
                    SqlCommand checkTeacherCmd = new SqlCommand(checkTeacherQuery, con);
                    checkTeacherCmd.Parameters.AddWithValue("@TeacherId", course.InstructorId);
                    int teacherCount = (int)checkTeacherCmd.ExecuteScalar();

                    if (teacherCount == 0)
                    {
                        throw new exception.TeacherNotFoundException();
                    }
                }

                string query = @"Update Courses set CourseName = @CourseName, CourseCode = @CourseCode, InstructorId = @InstructorId where CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", course.CourseID);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.Parameters.AddWithValue("@InstructorId", course.InstructorId > 0 ? (object)course.InstructorId : DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCourse(int courseId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "Select count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@CourseId", courseId);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Course with ID {courseId} not found");
                }

                string deleteEnrollmentsQuery = "Delete from Enrollments where CourseId = @CourseId";
                SqlCommand deleteEnrollmentsCmd = new SqlCommand(deleteEnrollmentsQuery, con);
                deleteEnrollmentsCmd.Parameters.AddWithValue("@CourseId", courseId);
                deleteEnrollmentsCmd.ExecuteNonQuery();

                string deleteCourseQuery = "delete from Courses where CourseId = @CourseId";
                SqlCommand deleteCourseCmd = new SqlCommand(deleteCourseQuery, con);
                deleteCourseCmd.Parameters.AddWithValue("@CourseId", courseId);
                deleteCourseCmd.ExecuteNonQuery();
            }
        }

        public Course GetCourseById(int courseId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {

                string query = "select * from Courses where CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", courseId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Course
                        {
                            CourseID = (int)reader["CourseId"],
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            InstructorId = reader["InstructorId"] != DBNull.Value ? (int)reader["InstructorId"] : 0
                        };
                    }
                    else
                    {
                        throw new exception.CourseNotFoundException();
                    }
                }
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "select * from Courses";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = (int)reader["CourseId"],
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            InstructorId = reader["InstructorId"] != DBNull.Value ? (int)reader["InstructorId"] : 0
                        });
                    }
                }
            }
            return courses;
        }

        public void AssignTeacherToCourse(int courseId, int teacherId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkCourseQuery = "Select Count(*) from Courses where CourseId = @CourseId";
                SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, con);
                checkCourseCmd.Parameters.AddWithValue("@CourseId", courseId);
                int courseCount = (int)checkCourseCmd.ExecuteScalar();

                if (courseCount == 0)
                {
                    throw new exception.CourseNotFoundException();
                }

                string checkTeacherQuery = "select count(*) from Teachers where TeacherId = @TeacherId";
                SqlCommand checkTeacherCmd = new SqlCommand(checkTeacherQuery, con);
                checkTeacherCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                int teacherCount = (int)checkTeacherCmd.ExecuteScalar();

                if (teacherCount == 0)
                {
                    throw new exception.TeacherNotFoundException();
                }

                string updateQuery = "update Courses set InstructorId = @TeacherId where CourseId = @CourseId";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@TeacherId", teacherId);
                updateCmd.Parameters.AddWithValue("@CourseId", courseId);

                updateCmd.ExecuteNonQuery();
            }
        }

        public List<Student> GetEnrolledStudents(int courseId)
        {
            List<Student> students = new List<Student>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = @"select s.StudentId, s.FirstName, s.LastName, s.DateOfBirth, s.Email, s.PhoneNumber from Students s join Enrollments e on s.StudentId = e.StudentId where e.CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", courseId);

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

        public Teacher GetCourseTeacher(int courseId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = @"Select t.TeacherId, t.FirstName, t.LastName, t.Email from Teachers t join Courses c on t.TeacherId = c.InstructorId where c.CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CourseId", courseId);

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
    }
}
