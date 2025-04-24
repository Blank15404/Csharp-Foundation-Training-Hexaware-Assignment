namespace StudentInformationSystem.entity
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int StudentID { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PaymentDate { get; set; }

        public Payment() { }

        public Payment(int paymentId, int studentId, decimal amount, System.DateTime paymentDate)
        {
            PaymentID = paymentId;
            StudentID = studentId;
            Amount = amount;
            PaymentDate = paymentDate;
        }
    }
}