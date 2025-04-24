using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentInformationSystem.dao.Interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;


namespace StudentInformationSystem.dao.Implementations
{
    public class PaymentServiceImpl : IPaymentServiceDao
    {
        public void AddPayment(Payment payment)
        {
            if (payment.Amount <= 0)
            {
                throw new exception.PaymentValidationException();
            }

            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkIdQuery = "Select count(*) from Payments where PaymentId = @PaymentId";
                SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, con);
                checkIdCmd.Parameters.AddWithValue("@PaymentId", payment.PaymentID);
                int idCount = (int)checkIdCmd.ExecuteScalar();

                if (idCount > 0)
                {
                    throw new exception.DuplicateIdException($"Payment with ID {payment.PaymentID} already exists");
                }

                string checkStudentQuery = "Select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", payment.StudentID);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string query = @"insert into Payments (PaymentId, StudentId, Amount, PaymentDate) 
                                values (@PaymentId, @StudentId, @Amount, @PaymentDate)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentID);
                cmd.Parameters.AddWithValue("@StudentId", payment.StudentID);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePayment(Payment payment)
        {
            if (payment.Amount <= 0)
            {
                throw new exception.PaymentValidationException();
            }

            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "select count(*) from Payments where PaymentId = @PaymentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@PaymentId", payment.PaymentID);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Payment with ID {payment.PaymentID} not found");
                }

                string checkStudentQuery = "Select count(*) from Students where StudentId = @StudentId";
                SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                checkStudentCmd.Parameters.AddWithValue("@StudentId", payment.StudentID);
                int studentCount = (int)checkStudentCmd.ExecuteScalar();

                if (studentCount == 0)
                {
                    throw new exception.StudentNotFoundException();
                }

                string query = @"Update Payments set StudentId = @StudentId, Amount = @Amount, PaymentDate = @PaymentDate where PaymentId = @PaymentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentID);
                cmd.Parameters.AddWithValue("@StudentId", payment.StudentID);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePayment(int paymentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string checkQuery = "select count(*) from Payments where PaymentId = @PaymentId";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    throw new exception.IdNotFoundException($"Payment with ID {paymentId} not found");
                }

                string deleteQuery = "delete from Payments where PaymentId = @PaymentId";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, con);
                deleteCmd.Parameters.AddWithValue("@PaymentId", paymentId);
                deleteCmd.ExecuteNonQuery();
            }
        }

        public Payment GetPaymentById(int paymentId)
        {
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Payments WHERE PaymentId = @PaymentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Payment
                        {
                            PaymentID = (int)reader["PaymentId"],
                            StudentID = (int)reader["StudentId"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        };
                    }
                    else
                    {
                        throw new exception.IdNotFoundException($"Payment with ID {paymentId} not found");
                    }
                }
            }
        }

        public List<Payment> GetAllPayments()
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Payments";
                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            PaymentID = (int)reader["PaymentId"],
                            StudentID = (int)reader["StudentId"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        });
                    }
                }
            }
            return payments;
        }

        public List<Payment> GetPaymentsByStudent(int studentId)
        {
            List<Payment> payments = new List<Payment>();
            using (SqlConnection con = DBUtility.GetConnection())
            {
                string query = "SELECT * FROM Payments WHERE StudentId = @StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            PaymentID = (int)reader["PaymentId"],
                            StudentID = (int)reader["StudentId"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        });
                    }
                }
            }
            return payments;
        }
    }
}