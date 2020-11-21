using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApi.Models;
using PokeApi.Repositories;
using PokeApi.Services.Interfaces;

namespace PokeApi.Services{
    
    public class PokemonService : IPokemonService{
    
        private readonly PokemonRepository pokemonRepository;

        public PokemonService(IPokedexDatabaseSettings settings){
            pokemonRepository = new PokemonRepository(settings);
        }
        
        public  async Task<List<Pokemon>> Get(){ 
            return await pokemonRepository.GetAllElements();    
        }

        public async Task<Pokemon> Get(string id){      
            return await pokemonRepository.GetElement(id);
        }

        public async Task<List<Pokemon>> GetCollection(IEnumerable<string> ids){
            return await pokemonRepository.GetElementsCollection(ids);
        }

        public async Task<Pokemon> Create(Pokemon poke){
            await pokemonRepository.PutElement(poke);
            return poke;
        }

        public async Task Update(string Id,Pokemon pokeIn){
            await pokemonRepository.Update(Id,pokeIn);
        }
        
        public void Remove(Pokemon pokeIn){
            pokemonRepository.DeleteElement(pokeIn.Id);
        }

        public void Remove(string Id){
            pokemonRepository.DeleteElement(Id);
        }        
    }
}