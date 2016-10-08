using MongoDB.Bson.Serialization.Attributes;

namespace AutoDriveDataModel.Models
{
    public class Student : BaseModel
    {
        [BsonElement("StudentCode")]
        public string StudentCode { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Gender")]
        public string Gender { get; set; }
        [BsonElement("DOB")]
        public string DOB { get; set; }
        [BsonElement("Suburbs")]
        public Suburb Suburbs { get; set; }
        [BsonElement("Mobile")]
        public string Mobile { get; set; }
        [BsonElement("PickUpLocation")]
        public string PickUpLocation { get; set; }
        [BsonElement("LicenceNumber")]
        public string LicenceNumber { get; set; }
        [BsonElement("LicenceState")]
        public string LicenceState { get; set; }
        [BsonElement("LicenceCountry")]
        public string LicenceCountry { get; set; }
        [BsonElement("LicenceExpireOn")]
        public string LicenceExpireOn { get; set; }
        [BsonElement("Instructor")]
        public StudentInstructor Instructor { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }
    }

    public class StudentInstructor : BaseModel
    {
        public string InstructorName { get; set; }
        public string InstructorCode { get; set; }
    }
}
