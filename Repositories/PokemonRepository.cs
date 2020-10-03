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
        private HashTable<Pokemon> pokemonTable;

        public PokemonRepository(IPokedexDatabaseSettings settings){
            this.settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            pokemonCollection = database.GetCollection<Pokemon>(settings.PokemonsCollectionsName);
            pokemonTable = new HashTable<Pokemon>(19);
            FillHashTable(pokemonCollection);
        }
        private void FillHashTable(IMongoCollection<Pokemon> _pokemonCollection){
            List<Pokemon> collection = _pokemonCollection.Find(pokemon => true).ToList();
            foreach(Pokemon element in collection){
                pokemonTable.AddElement(element);
            }
        }

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
        }

        public async Task<List<Pokemon>> GetAllElements(){
            return await pokemonCollection.Find(pokemon => true).ToListAsync();
        }

        public async Task<List<Pokemon>> GetElementsCollection(IEnumerable<string> elementIds){
            List<Pokemon> elements = new List<Pokemon>();
            foreach(string id in elementIds){
                elements.Add(await pokemonTable.GetElement(id));
                }
            return elements;
        }
    }
}