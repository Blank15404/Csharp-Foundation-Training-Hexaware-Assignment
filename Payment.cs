using System;

namespace Task_8_SIS
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(int id, Student student, decimal amount, DateTime date)
        {
            PaymentID = id;
            Student = student;
            Amount = amount;
            PaymentDate = date;
        }

        public Student GetStudent() => Student;
        public decimal GetPaymentAmount() => Amount;
        public DateTime GetPaymentDate() => PaymentDate;
    }
}