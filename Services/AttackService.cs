using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using PokeApi.Factories;
using PokeApi.Models;

namespace PokeApi.Services{
    public class AttackService{
        private readonly IMongoCollection<Attack> _attack;
        private readonly AttackFactory attackFactory;

        public AttackService(IPokedexDatabaseSettings settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _attack = database.GetCollection<Attack>(settings.AttacksCollectionsName);
            attackFactory = new AttackFactory(Get());
        }

        public List<Attack> Get(){
            if(attackFactory == null)
                return _attack.Find(attack=>true).ToList();
            return attackFactory.GetAllElements();

        }

        public Attack Get(string id){ 
            if(attackFactory == null)
                return _attack.Find<Attack>(attack => attack.Id == id).FirstOrDefault();

            return attackFactory.GetElement(id);
        }
        
        public List<Attack> GetAttackForPokemon(string PokeId){
            IEnumerable<Attack> query =
            from a in _attack.Find<Attack>(attack=>true).ToList()
            where a.PokemonsIds.Contains(PokeId)
            select a;

            return query.ToList();
        }
        
        public Attack Create(Attack attack){
            _attack.InsertOne(attack);
            attackFactory.PutElement(attack);
            return attack;
        }
        public void Update(string id, Attack attackIn){
            _attack.ReplaceOne(attack=>attack.Id == id,attackIn);
            attackFactory.PutElement(attackIn);
        }
        public void Remove(Attack attackIn){
            _attack.DeleteOne(attack=>attack.Id== attackIn.Id);
            attackFactory.DeleteElement(attackIn.Id);
        }
        public void Remove(string id){
            _attack.DeleteOne(attack=>attack.Id== id);
            attackFactory.DeleteElement(id);
        }
    }
}