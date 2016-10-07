﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace AutoDriveDataModel.Models
{
    public class Instructor : BaseModel
    {
        [BsonElement("InstructorCode")]
        public string InstructorCode { get; set; }
        [BsonElement("First Name")]
        public string FirstName { get; set; }
        [BsonElement("Last Name")]
        public string LastName { get; set; }
        [BsonElement("Gender")]
        public string Gender { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("DOB")]
        public string DOB { get; set; }
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

    public class Suburb : BaseModel
    {
        [BsonElement("Suburb")]
        public string SuburbName { get; set; }
        [BsonElement("PostCode")]
        public string PostCode { get; set; }
        [BsonElement("Display")]
        public string Display { get; set; }
    }   
}