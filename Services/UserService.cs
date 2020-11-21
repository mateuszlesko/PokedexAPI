using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using PokeApi.Entities;
using PokeApi.Models;
using PokeApi.Helpers;
using PokeApi.Repositories;

namespace PokeApi.Services{
    
    public class UserService:Interfaces.IUserService{
       
       private readonly UserRepository userRepository;       
       public UserService(IPokedexDatabaseSettings settings,IOptions<AppSettings> appSettings){
           userRepository = new UserRepository(settings, appSettings);
       }
       
       public User Authenticate(string login, string password){
           return userRepository.Authenticate(login,password);
       }

       public IEnumerable<User> GetAllUsers(){
           return userRepository.GetAll();
       }

       public User GetUserById(string id){
           return userRepository.GetById(id);
       }

       public bool CreateUser(User user){
           return userRepository.Create(user);
       }

    }
}