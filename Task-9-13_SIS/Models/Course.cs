using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7_11_SIS.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int? InstructorId { get; set; }

        public override string ToString()
        {
            return $"{CourseId}: {CourseName} ({CourseCode}) | Instructor ID: {InstructorId}";
        }
    }
}
