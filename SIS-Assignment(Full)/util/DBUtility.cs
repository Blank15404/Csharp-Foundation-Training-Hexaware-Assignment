using System;
using System.Data.SqlClient;

namespace StudentInformationSystem.util
{
    public static class DBUtility
    {
        private static readonly string connectionString = @"Server=DESKTOP-F473ICG\SQLEXPRESS;Database=SISDB;Integrated Security=True;MultipleActiveResultSets=true;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new exception.DatabaseConnectionException($"Error opening the connection: {ex.Message}");
            }
        }
    }
}
