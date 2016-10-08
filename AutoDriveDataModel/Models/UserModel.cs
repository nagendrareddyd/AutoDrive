using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Models
{
    public class UserModel : BaseModel
    {
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Role")]
        public string Role { get; set; }
        [BsonElement("InstructorCode")]
        public string InstructorCode { get; set; }
    }
}
