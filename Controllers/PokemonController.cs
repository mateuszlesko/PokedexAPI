using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : ControllerBase{
        
        private readonly PokemonService _pokemonService;
        private readonly UserService _userService;
        
        public PokemonController(PokemonService pokemonService,UserService userService){
            _userService = userService;
            _pokemonService = pokemonService;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Pokemon>> Get()=>
        _pokemonService.Get();
    
        [AllowAnonymous]
        [HttpGet("{id:length(24)}",Name="GetPokemon")]
        public ActionResult<Pokemon> Get(string id){
            var pokemon = _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            return pokemon;
        }
        [Authorize(Roles=Role.Admin)]
        [HttpPost]
        public ActionResult<Pokemon> Create(Pokemon pokemon){
            _pokemonService.Create(pokemon);
            return CreatedAtRoute("GetPokemon",new{ id = pokemon.Id.ToString()},pokemon);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id,Pokemon pokemonIn)
        {
            var pokemon = _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }

            _pokemonService.Update(id,pokemonIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var pokemon = _pokemonService.Get(id);
            if(pokemon == null){
                return NotFound();
            }
            _pokemonService.Remove(pokemon.Id);
            return NoContent();
        }
    }
}