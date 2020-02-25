using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeApi.Models{

    class Pokemon : IPokedexDatabaseSettings{
        
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
        public float Height {get;set;}

        [BsonElement("weight")]
        public string Weight {get;set;}

        [BsonElement("HP")]
        public int HP {get;set;}

        [BsonElement("attack")]
        public string Attack {get;set;}

        [BsonElement("defense")]
        public string Defense {get;set;}

        [BsonElement("speedAttack")]
        public string SpeedAttack {get;set;}

        [BsonElement("speedDefense")]
        public string SpeedDefense {get;set;}

        [BsonElement("speed")]
        public string Speed {get;set;}

        [BsonElement("modelUrl")]
        public string ModelUrl {get;set;}

        [BsonElement("texturesUrl")]
        public string TexturesUrl {get;set;}

    }
    public interface IPokedexDatabaseSettings{
        string PokemonsCollectionsName {get;set;}
        string ConnectionString {get;set;}
        string DatabaseName {get;set;}
    }
}