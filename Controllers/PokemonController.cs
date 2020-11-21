using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PokeApi.Models;
using PokeApi.Entities;
using PokeApi.Services;
using PokeApi.Helpers;

namespace PokeApi.Controllers{
    [Authorize]
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : ControllerBase{
        
        private readonly PokemonService _pokemonService;
        public PokemonController(IPokedexDatabaseSettings settings){
            _pokemonService = new PokemonService(settings);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> Get()=> await _pokemonService.Get();
    
        [AllowAnonymous]
        [HttpGet("{id:length(24)}",Name="GetPokemon")]
        public async Task<ActionResult<Pokemon>> Get(string id){
            var pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return pokemon;
        }

        [AllowAnonymous]
        [Route("image/logo/{id}",Name="GetLogoImg")]
         public async Task<IActionResult> GetLogoImage(string id){
            Pokemon pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return File(await ImageService.ReadImageAsync(pokemon.Name.ToLower(),"logo.png"),"image/png","logo.png");
        }

        [AllowAnonymous]
        [HttpGet("image/front/{id}",Name="GetFrontImg")]
        public async Task<ActionResult> GetFrontImage(string id){
            Pokemon pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return File(await ImageService.ReadImageAsync(pokemon.Name.ToLower(),"front.png"),"image/png","front.png");
        }

        [AllowAnonymous]
        [Route("image/back/{id}",Name="GetBackImg")]
         public async Task<IActionResult> GetBackImage(string id){
            Pokemon pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return File(await ImageService.ReadImageAsync(pokemon.Name.ToLower(),"back.png"),"image/png","back.png");
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("/api/pokemons/pokemonCollection")]
        public async Task<IEnumerable<Pokemon>> GetCollectionOfPokemon(IEnumerable<string> pokemonIds){
            IEnumerable<Pokemon> pokemons = await _pokemonService.GetCollection(pokemonIds);
            return pokemons;
        }
       
        [Authorize(Roles=Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<Pokemon>> Create(Pokemon pokemon){
            await _pokemonService.Create(pokemon);
            return CreatedAtRoute("GetPokemon",new{ id = pokemon.Id.ToString()},pokemon);
        }

        [Authorize(Roles=Role.Admin)]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id,Pokemon pokemonIn)
        {
            var pokemon = _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }

            await _pokemonService.Update(id,pokemonIn);

            return NoContent();
        }

        [Authorize(Roles=Role.Admin)]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id){
            var pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            _pokemonService.Remove(pokemon.Id);
            return NoContent();
        }
    }
}