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

        [BsonElement("generation")]
        public int Generation {get;set;}

        [BsonElement("type1")]
        public string type1 {get;set;}

        [BsonElement("type2")]
        public string type2 {get;set;}

        [BsonElement("species")]
        public string Species {get;set;}

        [BsonElement("height")]
        public decimal Height {get;set;}

        [BsonElement("weigth")]
        public decimal Weight {get;set;}

        [BsonElement("HP")]
        public int HP {get;set;}

        [BsonElement("attack")]
        public int Attack {get;set;}

        [BsonElement("defense")]
        public int Defense {get;set;}

        [BsonElement("speedAttack")]
        public int SpeedAttack {get;set;}

        [BsonElement("speedDefence")]
        public int SpeedDefense {get;set;}

        [BsonElement("speed")]
        public int Speed {get;set;}

        [BsonElement("previous")]
        public string Previous {get;set;}

        [BsonElement("next")]
        public string Next {get;set;}

        [BsonElement("modelUrl")]
        public string ModelUrl {get;set;}

        [BsonElement("texturesUrl")]
        public string TexturesUrl {get;set;}
        
        [JsonProperty("AttacksList")]
        public System.Collections.Generic.List<Attack> Attacks {get;set;}

        public System.Collections.Generic.List<Attack> GetAttacks()=>Attacks;

        public void SetAttacks( System.Collections.Generic.List<Attack> list){Attacks = list;}
}
}