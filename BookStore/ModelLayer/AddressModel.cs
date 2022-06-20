using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    [BsonIgnoreExtraElements]
    public class AddressModel
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string addressID { get; set; }


        [ForeignKey("RegisterModel")]
        public string userId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressType")]
         public string addressTypeID { get; set; }
        public string fullAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public double pinCode { get; set; }


    }
}
