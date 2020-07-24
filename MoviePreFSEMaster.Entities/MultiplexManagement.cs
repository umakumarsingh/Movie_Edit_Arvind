using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviePreFSEMaster.Entities
{
    public class MultiplexManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MultiplexID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
