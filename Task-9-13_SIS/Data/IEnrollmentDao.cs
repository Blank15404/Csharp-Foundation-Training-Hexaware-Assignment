using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal interface IEnrollmentDao
    {
        int EnrollStudentInCourse(int studentId, int courseId, DateTime enrollmentDate, int enrollmentId);
        List<Enrollment> GetEnrollmentsByCourseId(int courseId);
    }
}
