using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PokeApi.Models{

    public class Attack{
       
        [BsonElement("name")]
        public string Name{get;set;}

        [BsonElement("type")]
        public string Type{get;set;}

        [BsonElement("power")]
        // [BsonIgnore]
        public string Power{get;set;}
        
        [BsonElement("accuracy")]
        public string Accurancy{get;set;}

        [BsonElement("pp")]
        public string PP {get;set;}

        [BsonElement("contact")]
        public string Contact {get;set;}
    }
}