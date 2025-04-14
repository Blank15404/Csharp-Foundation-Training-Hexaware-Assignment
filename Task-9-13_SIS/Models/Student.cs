using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7_11_SIS.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"{StudentId}: {FirstName} {LastName} | DOB: {DateOfBirth.ToShortDateString()} | Email: {Email} | Phone: {PhoneNumber}";
        }
    }
}
