
namespace AutoDriveEntities
{
    public class StudentEntity
    {
        public string Id { get; set; }
        public string StudentCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public SuburbEntity Suburbs { get; set; }
        public string Mobile { get; set; }
        public string PickUpLocation { get; set; }
        public string LicenceNumber { get; set; }
        public string LicenceState { get; set; }
        public string LicenceCountry { get; set; }
        public string LicenceExpireOn { get; set; }
        public StudentInstructor Instructor { get; set; }
        public string Status { get; set; }
    }
    public class StudentInstructor
    {
        public string Id { get; set; }
        public string InstructorName { get; set; }
    }
}
