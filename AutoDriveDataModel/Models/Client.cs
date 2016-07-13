using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Models
{
    public class Client : BaseModel
    {
        [BsonElement("ClientId")]
        public string ClientId { get; set; }
        [BsonElement("Secret")]
        public string Secret { get; set; }
        [BsonElement("Name")]        
        public string Name { get; set; }
        [BsonElement("ApplicationType")]
        public string ApplicationType { get; set; }
        [BsonElement("Active")]
        public bool Active { get; set; }
        [BsonElement("RefreshTokenLifeTime")]
        public int RefreshTokenLifeTime { get; set; }
        [BsonElement("AllowedOrigin")]
        public string AllowedOrigin { get; set; }
    }
}
