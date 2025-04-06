using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_7_SIS
{
    // Custom Exceptions
    public class StudentNotFoundException : Exception { public StudentNotFoundException(string msg) : base(msg) { } }
    public class CourseNotFoundException : Exception { public CourseNotFoundException(string msg) : base(msg) { } }
    public class DuplicateEnrollmentException : Exception { public DuplicateEnrollmentException(string msg) : base(msg) { } }

    // Student Class
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    // Course Class
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Course(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    // Main System
    public class SIS
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();

        public void EnrollStudent(int studentId, int courseId)
        {
            var student = Students.FirstOrDefault(s => s.Id == studentId);
            var course = Courses.FirstOrDefault(c => c.Id == courseId);

            if (student == null)
                throw new StudentNotFoundException($"Student with ID {studentId} not found!");
            if (course == null)
                throw new CourseNotFoundException($"Course with ID {courseId} not found!");
            if (student.Courses.Any(c => c.Id == courseId))
                throw new DuplicateEnrollmentException($"{student.Name} is already enrolled in {course.Name}!");

            student.Courses.Add(course);
            Console.WriteLine($"Enrolled {student.Name} in {course.Name}");
        }
    }

    class Program
    {
        static void Main()
        {
            var sis = new SIS();

            // Add some sample data
            sis.Students.Add(new Student(1, "Alice"));
            sis.Students.Add(new Student(2, "Bob"));
            sis.Courses.Add(new Course(101, "Math"));
            sis.Courses.Add(new Course(102, "Science"));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("STUDENT ENROLLMENT SYSTEM");
                Console.WriteLine("1. Enroll student");
                Console.WriteLine("2. Exit");
                Console.Write("Choose option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Press any key...");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 2) break;

                if (choice == 1)
                {
                    try
                    {
                        Console.Write("\nEnter student ID (1 or 2): ");
                        int studentId = int.Parse(Console.ReadLine());

                        Console.Write("Enter course ID (101 or 102): ");
                        int courseId = int.Parse(Console.ReadLine());

                        sis.EnrollStudent(studentId, courseId);
                    }
                    catch (StudentNotFoundException ex)
                    {
                        Console.WriteLine($"\nERROR: {ex.Message}");
                    }
                    catch (CourseNotFoundException ex)
                    {
                        Console.WriteLine($"\nERROR: {ex.Message}");
                    }
                    catch (DuplicateEnrollmentException ex)
                    {
                        Console.WriteLine($"\nERROR: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nUNEXPECTED ERROR: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
