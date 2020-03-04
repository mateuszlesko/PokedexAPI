using PokeApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PokeApi.Services{
    
    public class PokemonService{
        
        private readonly IMongoCollection<Pokemon> _pokemons;
        public PokemonService(IPokedexDatabaseSettings settings){

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pokemons = database.GetCollection<Pokemon>(settings.PokemonsCollectionsName);
        }

        public List<Pokemon> Get()=> _pokemons.Find(poke=>true).ToList();
        

        public Pokemon Get(string id)=>_pokemons.Find<Pokemon>(poke => poke.Id == id).FirstOrDefault();
        

        public Pokemon Create(Pokemon poke){
            _pokemons.InsertOne(poke);
            return poke;
        }

        public void Update(string name,Pokemon pokeIn)=>_pokemons.ReplaceOne(poke=>poke.Name == name,pokeIn);
        
        public void Remove(Pokemon pokeIn)=>_pokemons.DeleteOne(poke=>poke.Name == pokeIn.Name);
        public void Remove(string name)=>_pokemons.DeleteOne(poke=>poke.Name == name);
        
        }
    }