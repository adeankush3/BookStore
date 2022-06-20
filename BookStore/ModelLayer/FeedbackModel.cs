using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class FeedbackModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string feedbackID { get; set; }

        public string Comment { get; set; }
        public decimal Rating { get; set; }


        [ForeignKey("RegisterModel")]
        public string userId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("BookModel")]
        public string BookId { get; set; }
        public virtual BookModel BookModel { get; set; }
    }
}
