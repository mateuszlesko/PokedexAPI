using PokeApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PokeApi.Services{
    public class AttackService{
        private readonly IMongoCollection<Attack> _attack;

        public AttackService(IPokedexDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _attack = database.GetCollection<Attack>(settings.AttacksCollectionsName);
        }

        public List<Attack> Get() => _attack.Find(attack=>true).ToList();

        public Attack Get(string id)=> _attack.Find<Attack>(attack => attack.Id == id).FirstOrDefault();

        public Attack Create(Attack attack){
            _attack.InsertOne(attack);
            return attack;
        }
        public void Update(string id, Attack attackIn) => _attack.ReplaceOne(attack=>attack.Id == id,attackIn);
        public void Remove(Attack attackIn) => _attack.DeleteOne(attack=>attack.Id== attackIn.Id);
        public void Remove(string id)=>_attack.DeleteOne(attack=>attack.Id== id);
    }
}