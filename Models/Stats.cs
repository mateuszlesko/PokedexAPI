using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PokeApi.Models{
    public class Stats{
        [BsonElement("HP")]
        public int HP {get;set;}

        [BsonElement("attack")]
        public int Attack {get;set;}

        [BsonElement("defense")]
        public int Defense {get;set;}

        [BsonElement("speedAttack")]
        public int SpeedAttack {get;set;}

        [BsonElement("speedDefense")]
        public int SpeedDefense {get;set;}

        [BsonElement("speed")]
        public int Speed {get;set;}
    }
}