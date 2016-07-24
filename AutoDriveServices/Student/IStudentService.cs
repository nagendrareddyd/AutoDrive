using AutoDriveEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices.Student
{
    public interface IStudentService
    {
        IEnumerable<StudentEntity> GetAllStudents();
        StudentEntity GetStudent(string id);
        StudentEntity GetStudentByCode(string code);
        bool Update(StudentEntity student);
        bool Save(StudentEntity sudent);
        bool Delete(string id);
    }
}
