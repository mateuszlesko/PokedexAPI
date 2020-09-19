using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PokeApi.Models{

    public class Graphic{
       
        [BsonElement("icon")]
        public string icon{get;set;}

        [BsonElement("front")]
        public string Front{get;set;}

        [BsonElement("back")]
        public string Back{get;set;}
        
    }
}