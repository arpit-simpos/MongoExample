using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoExample.Data
{
    public class Branch
    {
        [BsonId]
        public string BranchId { get; set; }
        public string BranchName { get; set; }
    }
}
