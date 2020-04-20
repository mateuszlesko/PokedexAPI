using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PokeApi.Entities;
using PokeApi.Models;
using PokeApi.Services;

namespace PokeApi.Controllers{
    [Authorize]
    [Route("api/attacks")]
    [ApiController]
    public class AttackController:ControllerBase{

        private readonly AttackService _attackService;

        public AttackController(AttackService attackService){
            _attackService = attackService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Attack>> Get()=>
            _attackService.Get();
        
        [AllowAnonymous]
        [HttpGet("{id:length(24)}",Name="GetAttack")]
        public ActionResult<Attack> Get(string id){
            var attack = _attackService.Get(id);
            if(attack==null){
                return NotFound();
            }
            return attack;
            
        }

        [Route("pokemon/{PokeId}")]
        [HttpGet("{PokeId:length(24)}",Name="GetPokemonAttack")]
        public ActionResult<List<Attack>> GetPokemonAttack(string PokeId){
        var attack = _attackService.GetAttackFromPokemon(PokeId);
            return attack;            
        }

        [Authorize(Roles=Role.Admin)]
        [HttpPost]
        public ActionResult<Attack> Create(Attack attack){
            _attackService.Create(attack);
            return CreatedAtRoute("GetAttack",new {id = attack.Id.ToString()},attack);
        }

        [Authorize(Roles=Role.Admin)]
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id,Attack attackIn){
            var attack = _attackService.Get(id);
            if(attack == null){
                return NotFound();
            }
            _attackService.Update(id,attackIn);

            return NoContent();
        }

        [Authorize(Roles=Role.Admin)]
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id){
            var attack = _attackService.Get(id);
            if(attack == null){
                return NotFound();
            }
            _attackService.Remove(attack.Id);
            return NoContent();
        }

    }

}