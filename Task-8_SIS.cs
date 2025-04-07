// Task8_Collections
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public Student(int id, string firstName, string lastName)
        {
            StudentID = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public Teacher Instructor { get; set; }

        public Course(int id, string name)
        {
            CourseID = id;
            CourseName = name;
        }
    }

    public class Enrollment
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(Student student, Course course, DateTime date)
        {
            Student = student;
            Course = course;
            EnrollmentDate = date;
        }
    }

    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public Teacher(int id, string firstName, string lastName)
        {
            TeacherID = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class Payment
    {
        public Student Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment(Student student, decimal amount, DateTime date)
        {
            Student = student;
            Amount = amount;
            PaymentDate = date;
        }
    }

    public class SIS
    {
        // Collections to store all entities
        public Dictionary<int, Student> Students { get; set; } = new Dictionary<int, Student>();
        public Dictionary<int, Course> Courses { get; set; } = new Dictionary<int, Course>();
        public Dictionary<int, Teacher> Teachers { get; set; } = new Dictionary<int, Teacher>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        //Methods
        public void AddEnrollment(Student student, Course course, DateTime date)
        {
            var enrollment = new Enrollment(student, course, date);
            Enrollments.Add(enrollment);
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

        public void AssignTeacherToCourse(Course course, Teacher teacher)
        {
            if (course.Instructor != null)
            {
                course.Instructor.AssignedCourses.Remove(course);
            }
            course.Instructor = teacher;
            teacher.AssignedCourses.Add(course);
        }

        public void AddPayment(Student student, decimal amount, DateTime date)
        {
            var payment = new Payment(student, amount, date);
            Payments.Add(payment);
            student.Payments.Add(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            return student.Enrollments;
        }

        public List<Course> GetCoursesForTeacher(Teacher teacher)
        {
            return teacher.AssignedCourses;
        }
    }

    class Program
    {
        static void Main()
        {
            var sis = new SIS();

            //sample data
            var student1 = new Student(1, "John", "Doe");
            var student2 = new Student(2, "Jane", "Smith");
            
            var course1 = new Course(101, "Mathematics");
            var course2 = new Course(102, "Computer Science");
            
            var teacher1 = new Teacher(1001, "Dr.", "Williams");
            var teacher2 = new Teacher(1002, "Prof.", "Johnson");

            // Add to collections
            sis.Students.Add(student1.StudentID, student1);
            sis.Students.Add(student2.StudentID, student2);
            sis.Courses.Add(course1.CourseID, course1);
            sis.Courses.Add(course2.CourseID, course2);
            sis.Teachers.Add(teacher1.TeacherID, teacher1);
            sis.Teachers.Add(teacher2.TeacherID, teacher2);
            
            // 1. Enroll students
            sis.AddEnrollment(student1, course1, DateTime.Now);
            sis.AddEnrollment(student2, course2, DateTime.Now.AddDays(-1));
            Console.WriteLine("\nEnrollments created successfully!");

            // 2. Assign teachers
            sis.AssignTeacherToCourse(course1, teacher1);
            sis.AssignTeacherToCourse(course2, teacher2);
            Console.WriteLine("Teachers assigned to courses!");

            // 3. Record payments
            sis.AddPayment(student1, 500.00m, DateTime.Now);
            sis.AddPayment(student2, 750.00m, DateTime.Now);
            Console.WriteLine("Payments recorded!");
        }
    }
}
