using System.Collections.Generic;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.Interfaces
{
    public interface ITeacherServiceDao
    {
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int teacherId);
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetAllTeachers();
        List<Course> GetAssignedCourses(int teacherId);
    }
}