using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePreFSEMaster.Entities
{
    public class AllotMovie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MultiplexID { get; set; }
        public string MovieName { get; set; }
        public string MultiplexName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
