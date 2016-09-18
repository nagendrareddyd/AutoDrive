using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Models
{
    public class Booking : BaseModel
    {
        [BsonElement("Instructor")]
        public BookingInstructor Instructor { get; set; }
        [BsonElement("Student")]
        public BookingStudent Student { get; set; }
        [BsonElement("StartDateTime")]
        public DateTime StartDateTime { get; set; }
        [BsonElement("EndDateTime")]
        public DateTime EndDateTime { get; set; }
        [BsonElement("PickUplocation")]
        public string PickUplocation { get; set; }
        [BsonElement("ContactNumber")]
        public string ContactNumber { get; set; }
        [BsonElement("Type")]
        public string Type { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }
        [BsonElement("DateCreatedOn")]
        public DateTime DateCreatedOn { get; set; }
        [BsonElement("DateModified")]
        public DateTime DateModified { get; set; }
        [BsonElement("CreatedBy")]
        public string CreatedBy { get; set; }
        [BsonElement("ModifiedBy")]
        public string ModifiedBy { get; set; }
    }

    public class BookingInstructor : BaseModel
    {
        public string InstructorName { get; set; }
    }
    public class BookingStudent : BaseModel
    {
        public string StudentName { get; set; }
    }
}
