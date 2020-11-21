using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeApi.Helpers;
using PokeApi.Entities;
using PokeApi.Models;
using PokeApi.Services;
using PokeApi.Services.Interfaces;

namespace PokeApi.Controllers{
   
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController:ControllerBase{

        private UserService _userService;
        
        public UserController(IPokedexDatabaseSettings settings, IOptions<AppSettings> appSettings){
            _userService = new UserService(settings,appSettings);
        }

        [AllowAnonymous]
        [HttpPost("createAccount")]
        public ActionResult<User> Create([FromBody] AuthenticateModel model){
            User userCreate = new User(){
                Login = model.Login,
                Password = model.Password,
                Role = "user",
            };
            bool result = _userService.CreateUser(userCreate);
            if(!result)
                return BadRequest(new {message = "Login was taken by someone else"});
            else
                userCreate = _userService.Authenticate(model.Login, model.Password);
            return userCreate;
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
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}",Name="GetById")]
        public IActionResult GetById(string id){

            var currentUserId = User.Identity.Name;
            if(id != currentUserId && !User.IsInRole(Role.Admin)){
                return Forbid();
            }

            var user = _userService.GetUserById(id);
            if(user == null){
                return NotFound();
            }
            
            return Ok(user);
        }
        // [HttpGet("isAdmin/{id}",Name="IsAdmin")]
        // public string IsAdmin(string id){
        //     var user = _userService.IsAdmin(id);
        //     return ""+user;
        // }
    }
}