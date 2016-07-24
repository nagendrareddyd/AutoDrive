using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Models
{
    public class Student : BaseModel
    {
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public Suburb Suburbs { get; set; }
        public string Mobile { get; set; }
        public string PickUpLocation { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceState { get; set; }
        public string LicenceCountry { get; set; }
        public string LicenceExpireOn { get; set; }
        public StudentInstructor Instructor { get; set; }
        public string Status { get; set; }
    }

    public class StudentInstructor : BaseModel
    {
        public string InstructorName { get; set; }
    }
}
