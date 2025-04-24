using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.Interfaces
{
    public interface IPaymentServiceDao
    {
        void AddPayment(Payment payment);
        void UpdatePayment(Payment payment);
        void DeletePayment(int paymentId);
        Payment GetPaymentById(int paymentId);
        List<Payment> GetAllPayments();
        List<Payment> GetPaymentsByStudent(int studentId);
    }
}