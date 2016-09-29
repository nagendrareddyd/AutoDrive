using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AutoDriveDataModel.Models
{
    public class Instructor : BaseModel
    {
        [BsonElement("InstructorCode")]
        public string InstructorCode { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Gender")]
        public string Gender { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Mobile")]
        public string Mobile { get; set; }
        [BsonElement("Home")]
        public string Home { get; set; }
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Suburb")]
        public Suburb Suburb { get; set; }
        [BsonElement("Areas")]
        public List<Area> Areas { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }
    }

    public class Suburb
    {
        [BsonElement("SuburbName")]
        public string SuburbName { get; set; }
        [BsonElement("PostalCode")]
        public string PostalCode { get; set; }        
    }   
}