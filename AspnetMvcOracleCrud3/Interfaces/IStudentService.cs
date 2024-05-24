using AspnetMvcOracleCrud3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetMvcOracleCrud3.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();

        Student GetStudentById(int id);

        void AddStudent(Student student);
        void EditStudent(Student student);
        void DeleteStudent(Student student);
    }
}
