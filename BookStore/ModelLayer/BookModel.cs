using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Rating { get; set; }
        public string RatingCount { get; set; }
        public string DiscountPrice { get; set; }
        public string ActualPrice { get; set; }
        public string Description { get; set; }
        public string BookImage { get; set; }
        public string BookQuantity { get; set; }
    }
}
