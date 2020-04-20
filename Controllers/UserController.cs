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
        public IActionResult GetById(string id){

            var currentUserId = User.Identity.Name;
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }

            var user = _userService.GetById(id);
            if(user == null){
                return NotFound();
            }
            
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("",Name="Create")]
        public IActionResult Create(User user){
            _userService.Create(user);
            return Ok(user);
        }

        [HttpDelete("",Name="Delete")]
        public IActionResult Remove(string id){
            var currentUserId = User.Identity.Name;
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }
            _userService.Remove(id);

            return Ok(null);
        }
        [HttpPut("",Name="Update")]
        public IActionResult Update(string id, User user){
            var currentUserId = User.Identity.Name;
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }
            _userService.Update(id,user);
            user.Password = null;
            user.Token = null;
            return Ok(user);
        }
    }
}