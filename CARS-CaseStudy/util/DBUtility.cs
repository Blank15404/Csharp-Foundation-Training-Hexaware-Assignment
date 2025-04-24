using System;
using System.Data.SqlClient;
using System.Data;

namespace CARS_CaseStudy.util
{
    public static class DBUtility
    {
        static readonly string connectionString = @"Server=DESKTOP-F473ICG\SQLEXPRESS;Database=CrimeAnalysisDB;Integrated Security=True;MultipleActiveResultSets=true;";

        public static SqlConnection GetConnection()
        {
            SqlConnection ConnectionObject = new SqlConnection(connectionString);
            try
            {
                ConnectionObject.Open();
                return ConnectionObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Opening the Connection : {ex.Message}");
                return null;
            }
        }
    }
}