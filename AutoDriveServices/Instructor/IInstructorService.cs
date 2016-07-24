using AutoDriveEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveServices.Instructor
{
    public interface IInstructorService
    {
        IEnumerable<InstructorEntity> GetAllInstructors();
        InstructorEntity GetInstructor(string id);
        InstructorEntity GetInstructorByCode(string code);
        bool Update(InstructorEntity instructor);
        bool Save(InstructorEntity instructor);
        bool Delete(string id);
    }
}
