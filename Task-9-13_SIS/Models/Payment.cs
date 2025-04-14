using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7_11_SIS.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public override string ToString()
        {
            return $"{PaymentId}: Student {StudentId} paid {Amount:C} on {PaymentDate.ToShortDateString()}";
        }
    }
}
