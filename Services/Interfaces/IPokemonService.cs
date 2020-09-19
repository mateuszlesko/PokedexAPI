using System.Collections.Generic;
using System.Threading.Tasks;
using PokeApi.Models;
namespace PokeApi.Services.Interfaces{
    public interface IPokemonService{
        List<Pokemon> Get();
        List<Pokemon> GetCollection(IEnumerable<string> ids);
        Task<Pokemon> Get(string id);
        Task<Pokemon> Create(Pokemon poke);
        Task Update(string Id,Pokemon pokeIn);
        void Remove(Pokemon pokeIn);
        void Remove(string Id);
    }
}