using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApi.Models;
using PokeApi.Repositories;

namespace PokeApi.Services{
    
    public class PokemonService : Interfaces.IPokemonService{
    
        private readonly PokemonRepository pokemonRepository;

        public PokemonService(IPokedexDatabaseSettings settings){
            pokemonRepository = new PokemonRepository(settings);
        }
        
        public  List<Pokemon> Get(){ 
            return pokemonRepository.GetAllElements();    
        }

        public async Task<Pokemon> Get(string id){      
            return await pokemonRepository.GetElement(id);
        }

        public List<Pokemon> GetCollection(IEnumerable<string> ids){
            return pokemonRepository.GetElementsCollection(ids);
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