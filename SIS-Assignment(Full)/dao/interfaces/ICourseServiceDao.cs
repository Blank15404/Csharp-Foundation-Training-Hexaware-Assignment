using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.Interfaces
{
    public interface ICourseServiceDao
    {
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        Course GetCourseById(int courseId);
        List<Course> GetAllCourses();
        void AssignTeacherToCourse(int courseId, int teacherId);
        List<Student> GetEnrolledStudents(int courseId);
        Teacher GetCourseTeacher(int courseId);
    }
}
