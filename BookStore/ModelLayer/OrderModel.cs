using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    [BsonIgnoreExtraElements]
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string orderID { get; set; }
       
        [ForeignKey("BookModel")]
        public string BookId { get; set; }
        public virtual BookModel BookModel { get; set; }

        [ForeignKey("RegisterModel")]
        public string userId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("AddressModel")]
        public string addressID { get; set; }
        public virtual AddressModel AddressModel { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

        // public int DiscountPrice { get; set; }
        // public int ActualPrice { get; set; }


    }
}
