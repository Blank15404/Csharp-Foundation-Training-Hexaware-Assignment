using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_7_11_SIS.Models;

namespace Task_7_11_SIS.Data
{
    internal interface ITeacherDao
    {
        int AddTeacher(Teacher teacher);
        Teacher GetTeacherById(int id);
    }
}
