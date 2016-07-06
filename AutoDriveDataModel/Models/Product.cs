﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoDriveDataModel.Models
{    
    public class Product : BaseModel
    {        
        [BsonElement("ProductId")]
        public int ProductId { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("Price")]
        public int Price { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
    }
}