using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PokeApi.Entities;
using PokeApi.Services;

namespace PokeApi.Controllers{
   
    [Route("api/users")]
    // [Authorize]
    [ApiController]
    public class UserController:ControllerBase{
        private IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<User> Authenticate( [FromBody] User userParam){
            var user = _userService.Authenticate(userParam.Login,userParam.Password);

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
            var user = _userService.GetById(id);
            if(user == null){
                return NotFound();
            }
            var currentUserId = int.Parse(User.Identity.Name);
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }
            return Ok(user);
        }
    }
}