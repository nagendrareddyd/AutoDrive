using AutoDriveEntities;
using System.Collections.Generic;

namespace AutoDriveServices.Instructor
{
    public interface IInstructorService
    {
        IEnumerable<InstructorEntity> GetAllInstructors();
        InstructorEntity GetInstructor(string id);
        InstructorEntity GetInstructorByCode(string code);
        string GetInstructorCode();
        bool Update(InstructorEntity instructor);
        bool Save(InstructorEntity instructor);
        bool Delete(string id);
    }
}
