using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal class TeacherDaoImpl : ITeacherDao
    {
        public int AddTeacher(Teacher teacher)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Teachers (TeacherId, FirstName, LastName, Email) values (@TeacherId, @FirstName, @LastName, @Email)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@TeacherId", teacher.TeacherId);
                    cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                    cmd.Parameters.AddWithValue("@Email", teacher.Email);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Teacher could not be added.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding a teacher.", ex);
            }

            return rowsAffected;
        }

        public Teacher GetTeacherById(int id)
        {
            Teacher teacher = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Teachers where TeacherId = @TeacherId";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@TeacherId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teacher = new Teacher
                            {
                                TeacherId = (int)reader["TeacherId"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Email = (string)reader["Email"]
                            };
                        }
                    }
                }

                if (teacher == null)
                {
                    throw new SISException($"Teacher with ID {id} not found.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching teacher with the given ID.", ex);
            }

            return teacher;
        }

    }
}
