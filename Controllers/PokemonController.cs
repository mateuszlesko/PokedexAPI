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
        public PokemonController(PokemonService pokemonService,UserService userService){
            _pokemonService = pokemonService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Pokemon>> Get()=> _pokemonService.Get();
    
        [AllowAnonymous]
        [HttpGet("{id:length(24)}",Name="GetPokemon")]
        public async Task<ActionResult<Pokemon>> Get(string id){
            var pokemon = await _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return pokemon;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Pokemon>> GetCollectionOfPokemon(IEnumerable<string> pokemonIds){
            List<Pokemon> pokemonCollection = null;
            return pokemonCollection;
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