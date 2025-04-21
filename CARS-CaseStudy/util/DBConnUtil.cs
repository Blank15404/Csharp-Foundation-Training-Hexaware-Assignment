using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CARS_CaseStudy.util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error opening database connection: {ex.Message}");
                return null;
            }
        }
    }
}
