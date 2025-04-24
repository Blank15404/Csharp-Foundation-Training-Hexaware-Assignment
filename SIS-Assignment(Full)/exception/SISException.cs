using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.exception
{
    public class SISException : Exception
    {
        public SISException(string message) : base(message) { }
    }

    public class DuplicateIdException : SISException
    {
        public DuplicateIdException(string message) : base(message) { }
    }

    public class IdNotFoundException : SISException
    {
        public IdNotFoundException(string message) : base(message) { }
    }

    public class DuplicateEnrollmentException : SISException
    {
        public DuplicateEnrollmentException() : base("Student is already enrolled in this course") { }
    }

    public class CourseNotFoundException : SISException
    {
        public CourseNotFoundException() : base("Course with this ID doesn't exists") { }
    }

    public class StudentNotFoundException : SISException
    {
        public StudentNotFoundException() : base("Student with this ID doesn't exists") { }
    }

    public class TeacherNotFoundException : SISException
    {
        public TeacherNotFoundException() : base("Teacher with this ID doesn't exists") { }
    }

    public class PaymentValidationException : SISException
    {
        public PaymentValidationException() : base("Invalid payment amount, make sure you have the correct payment detail") { }
    }

    public class InvalidStudentDataException : SISException
    {
        public InvalidStudentDataException(string message) : base($"Invalid student data: {message}") { }
    }

    public class InvalidCourseDataException : SISException
    {
        public InvalidCourseDataException(string message) : base($"Invalid course data: {message}") { }
    }

    public class InvalidEmailException : SISException
    {
        public InvalidEmailException() : base("Invalid email format") { }
    }

    public class InvalidEnrollmentDataException : SISException
    {
        public InvalidEnrollmentDataException() : base("Invalid enrollment data") { }
    }

    public class InvalidTeacherDataException : SISException
    {
        public InvalidTeacherDataException(string message) : base($"Invalid teacher data: {message}") { }
    }

    public class DatabaseConnectionException : SISException
    {
        public DatabaseConnectionException(string message) : base($"Database connection error: {message}") { }
    }
}
