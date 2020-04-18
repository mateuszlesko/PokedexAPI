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
    [ApiController]
    [Route("api/users")]
    public class UserController:ControllerBase{
        private IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate( [FromBody] AuthenticateModel model){
            var user = _userService.Authenticate(model.Login,model.Password);

            if(user == null){
                return BadRequest(new {message = "Login or password is incorrect"});
            }
            return Ok(user);
        }

        [Authorize(Roles=Role.Admin)]
        [HttpGet]
        public IActionResult GetAll(){
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}",Name="GetById")]
        public IActionResult GetById(int id){

            var currentUserId = int.Parse(User.Identity.Name);
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }

            var user = _userService.GetById(id);
            if(user == null){
                return NotFound();
            }
            
            return Ok(user);
        }
        [HttpGet("isAdmin/{id}",Name="IsAdmin")]
        public string IsAdmin(int id){
            var user = _userService.IsAdmin(id);
            return ""+user;
        }
    }
}