using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApi.Models;
using MongoDB.Driver;
using PokeApi.Repositories.Interfaces;
using PokeApi.Helpers;
using PokeApi.Helpers.DataStructures;
namespace PokeApi.Repositories{

    public class PokemonRepository:IModelRepository<Pokemon>{
        
        private readonly IMongoCollection<Pokemon> pokemonCollection;
        private IPokedexDatabaseSettings settings;
        private List<Pokemon> pokemons;

        private HashTable<Pokemon> pokemonTable;

        public PokemonRepository(IPokedexDatabaseSettings settings){
            this.settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            pokemonCollection = database.GetCollection<Pokemon>(settings.PokemonsCollectionsName);
            
            pokemons = pokemonCollection.Find(pokemon => true).ToList();
            pokemonTable = new HashTable<Pokemon>(19);
        }
        private void FillHashTable(List<Pokemon> collection){
            foreach(Pokemon element in collection){
                pokemonTable.AddElement(element);
            }
        }

        public Boolean IsEmpty(){return pokemons == null;}

        public async Task PutElement(Pokemon model)
        {
            await pokemonCollection.InsertOneAsync(model); 
        }
        public void DeleteElement(String Id){
            pokemonCollection.DeleteOne(poke=>poke.Id.Equals(Id));
        }
        public async Task<Pokemon> GetElement(String id) {
            Pokemon pokemon = await pokemonCollection.Find(poke => poke.Id == id).FirstOrDefaultAsync();
            if(pokemon == null)
                return null;
            return pokemon;
        }

        public async Task Update(string Id, Pokemon pokemon ){
            await pokemonCollection.ReplaceOneAsync(poke=>poke.Id == Id,pokemon);
            await PutElement(pokemon);
        }

        public List<Pokemon> GetAllElements(){
            return pokemons;
        }

        public List<Pokemon> GetElementsCollection(IEnumerable<string> elementIds){
            List<Pokemon> elements = new List<Pokemon>();
            foreach(string id in elementIds)
                elements.Add(pokemons.FirstOrDefault(element => element.Id == id));
            return elements;
        }
    }
}