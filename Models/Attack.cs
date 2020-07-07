using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace PokeApi.Models{

    public class Attack{
       
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get;set;}

        [BsonElement("name")]
        public string Name{get;set;}

        [BsonElement("effects")]
        public string Effects{get;set;}
        
        [BsonElement("type")]
        public string Type{get;set;}

        [BsonElement("category")]
        public string  Category {get;set;} 

        [BsonElement("power")]
        public int Power{get;set;}
        
        [BsonElement("accurancy")]
        public int Accurancy{get;set;}

        [BsonElement("pp")]
        public int PP {get;set;}
        
        [BsonElement("pokemonsIds")]
        public List<string> PokemonsIds {get;set;}
    }
}