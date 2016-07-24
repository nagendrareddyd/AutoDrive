using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string SuburbName { get; set; }
        public string PostCode { get; set; }
    }
}
