using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PokeApi.Models;
using System.Net.Http;
using PokeApi.Factories;
using System.Text.Json;

namespace PokeApi.Services{
    
    public class PokemonService{
        
        private readonly IMongoCollection<Pokemon> _pokemons;
        private readonly PokemonFactory pokemonFactory;
        private readonly IHttpClientFactory _clientFactory;
        public PokemonService(IPokedexDatabaseSettings settings,IHttpClientFactory clientFactor){

            _clientFactory = clientFactor;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pokemons = database.GetCollection<Pokemon>(settings.PokemonsCollectionsName);
            pokemonFactory = new PokemonFactory(Get());
        }

        public List<Pokemon> Get(){ 
            if(pokemonFactory == null)
                return _pokemons.Find(poke=>true).ToList();

            return pokemonFactory.GetAllElements();    
        }

        public Pokemon Get(string id){
            if(pokemonFactory == null)
                return _pokemons.Find<Pokemon>(poke => poke.Id == id).FirstOrDefault();
                
            return pokemonFactory.GetElement(id);
        }
        public Pokemon Create(Pokemon poke){
            _pokemons.InsertOne(poke);
            return poke;
        }

        public void Update(string Id,Pokemon pokeIn){
            _pokemons.ReplaceOne(poke=>poke.Id == Id,pokeIn);
            pokemonFactory.PutElement(pokeIn);
        }
        
        public void Remove(Pokemon pokeIn){
            _pokemons.DeleteOne(poke=>poke.Name == pokeIn.Name);
            pokemonFactory.DeleteElement(pokeIn.Id);
        }

        public void Remove(string Id){
            _pokemons.DeleteOne(poke=>poke.Id == Id);
            pokemonFactory.DeleteElement(Id);
        }        
    }
}