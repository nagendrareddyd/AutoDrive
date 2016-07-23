using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDriveDataModel.Models
{
    public class Area : BaseModel
    {
        [BsonElement("AreaCode")]
        public string AreaCode { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
