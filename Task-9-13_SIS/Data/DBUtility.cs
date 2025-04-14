using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Task_7_11_SIS.Data
{
    internal class DBUtility
    {
        static readonly string connectionString = @"Server=DESKTOP-F473ICG\SQLEXPRESS; Database=SIS; Integrated Security=True; MultipleActiveResultSets=true;";

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

        public static void CloseDbConnection(SqlConnection ConnectionObject)
        {
            if (ConnectionObject != null)
            {
                try
                {
                    if (ConnectionObject.State != ConnectionState.Open)
                    {
                        ConnectionObject.Close();
                        ConnectionObject.Dispose();
                        Console.WriteLine("Connection Closed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Closing connection: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Connection is already null");
            }
        }
    }
}
