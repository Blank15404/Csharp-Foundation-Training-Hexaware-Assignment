using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal class PaymentDaoImpl : IPaymentDao
    {
        public int RecordPayment(Payment payment, int paymentId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            int rowsAffected = 0;

            string query = @"insert into Payments (PaymentId, StudentId, Amount, PaymentDate) values (@PaymentId, @StudentId, @Amount, @PaymentDate)";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PaymentId", paymentId);
                    cmd.Parameters.AddWithValue("@StudentId", payment.StudentId);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected <= 0)
                {
                    throw new Exception("Payment could not be recorded.");
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in recording payment.", ex);
            }

            return rowsAffected;
        }


        public List<Payment> GetPaymentsByStudentId(int studentId)
        {
            List<Payment> payments = new List<Payment>();
            SqlConnection con = null;
            SqlCommand cmd = null;

            string query = "select * from Payments WHERE StudentId = @StudentId ORDER BY PaymentDate DESC";

            try
            {
                using (con = DBUtility.GetConnection())
                {
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(new Payment
                            {
                                PaymentId = (int)reader["PaymentId"],
                                StudentId = (int)reader["StudentId"],
                                Amount = (decimal)reader["Amount"],
                                PaymentDate = (DateTime)reader["PaymentDate"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payments for student.", ex);
            }

            return payments;
        }
    }
}
