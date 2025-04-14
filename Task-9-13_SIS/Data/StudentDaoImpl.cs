using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal class StudentDaoImpl : IStudentDao
    {
        public int AddStudent(Student student)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Students (StudentId, FirstName, LastName, DateOfBirth, Email, PhoneNumber) values (@StudentId, @FirstName, @LastName, @DateOfBirth, @Email, @PhoneNumber)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Student could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding a student.", ex);
            }

            return rowsAffected;
        }

        public Student GetStudentById(int id)
        {
            Student student = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Students where StudentId = @StudentId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentId = (int)reader["StudentId"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Email = (string)reader["Email"],
                                PhoneNumber = reader["PhoneNumber"] as string
                            };
                        }
                    }
                }

                if (student == null)
                {
                    throw new SISException($"Student with ID {id} not found.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching student with the given ID.", ex);
            }

            return student;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Students";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = (int)reader["StudentId"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                Email = (string)reader["Email"],
                                PhoneNumber = reader["PhoneNumber"] as string
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all students.", ex);
            }

            return students;
        }

        public int UpdateStudent(Student student)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"update Students set FirstName = @FirstName, LastName = @LastName, 
                            DateOfBirth = @DateOfBirth, Email = @Email, PhoneNumber = @PhoneNumber where StudentId = @StudentId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("No student found with the given ID to update.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating student.", ex);
            }

            return rowsAffected;
        }
    }
}
