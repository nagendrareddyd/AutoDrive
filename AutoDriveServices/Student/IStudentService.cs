using AutoDriveEntities;
using System.Collections.Generic;

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
        IEnumerable<StudentEntity> GetStudentsByINSCode(string code);
    }
}
