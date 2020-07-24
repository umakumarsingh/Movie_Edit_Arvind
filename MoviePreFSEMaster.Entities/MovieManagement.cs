using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePreFSEMaster.Entities
{
   public class MovieManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MovieId { get; set; }
        public string DirectedBy { get; set; }
        public string Producer { get; set; }
        public string Production { get; set; }
        public DateTime ReleasedDate { get; set; }

    }
}
