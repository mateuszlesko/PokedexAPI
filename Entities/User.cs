using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokeApi.Entities{
    public class User{
        public int Id{get;set;}
        public string Login{get;set;}
        public string Password{get;set;}
        public string Role {get;set;}
        public string Token{get;set;}
    }
}