using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS
{
    internal class UserInterface
    {
        public int GetStudentId()
        {
            Console.Write("Enter Student ID: ");
            return int.Parse(Console.ReadLine());
        }

        public string GetFirstName()
        {
            Console.Write("Enter First Name: ");
            return Console.ReadLine();
        }

        public string GetLastName()
        {
            Console.Write("Enter Last Name: ");
            return Console.ReadLine();
        }

        public DateTime GetDateOfBirth()
        {
            Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
            return DateTime.Parse(Console.ReadLine());
        }

        public string GetEmail()
        {
            Console.Write("Enter Email: ");
            return Console.ReadLine();
        }

        public string GetPhoneNumber()
        {
            Console.Write("Enter Phone Number: ");
            return Console.ReadLine();
        }

        public decimal GetPaymentAmount()
        {
            Console.Write("Enter Payment Amount: ");
            return decimal.Parse(Console.ReadLine());
        }

        public int GetEnrollmentId()
        {
            Console.Write("Enter Enrollment ID: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetPaymentId()
        {
            Console.Write("Enter Payment ID: ");
            return int.Parse(Console.ReadLine());
        }

        public int GetCourseCount()
        {
            Console.Write("How many courses to enroll in? ");
            return int.Parse(Console.ReadLine());
        }

        public int GetCourseId(string prompt = "Enter Course ID: ")
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine());
        }

        public int GetTeacherId()
        {
            Console.Write("Enter Teacher ID: ");
            return int.Parse(Console.ReadLine());
        }
    }
}