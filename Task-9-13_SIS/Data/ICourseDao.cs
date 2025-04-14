using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal interface ICourseDao
    {
        int AddCourse(Course course);
        Course GetCourseById(int id);
        List<Course> GetAllCourses();
        int AssignTeacherToCourse(int courseId, int teacherId);
    }
}
