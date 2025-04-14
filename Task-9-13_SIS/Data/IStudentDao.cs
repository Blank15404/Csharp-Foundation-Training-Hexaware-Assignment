using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal interface IStudentDao
    {
        int AddStudent(Student student);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        int UpdateStudent(Student student);
    }
}
