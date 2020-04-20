using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeApi.Entities{
    public class User{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get;set;}
        
        [BsonElement("login")]
        public string Login{get;set;}
        
        [BsonElement("password")]
        public string Password{get;set;}
        
        [BsonElement("role")]
        public string Role {get;set;}
        
        [BsonElement("token")]
        public string Token{get;set;}
    }
}