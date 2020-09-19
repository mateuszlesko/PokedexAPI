using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PokeApi.Models{

    public class Pokemon{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get;set;}

        [BsonElement("name")]
        public string Name {get;set;}

        [BsonElement("type")]
        public string type {get;set;}
        [BsonElement("stats")]
        public Stats Stats {get;set;}
        public string Next {get;set;}
        [BsonElement("graphics")]
        public Graphic Graphic {get;set;}
        [BsonElement("attacks")]
        public System.Collections.Generic.List<Attack> Attacks {get;set;}

    }
}