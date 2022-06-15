using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class RegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string userId { get; set; }
        public string fullName { get; set; }
        public string emailID { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }

    }
}
