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

namespace PokeApi.Services{
    
    public interface IUserService{
        User Authenticate(string login,string password);
        IEnumerable<User> GetAll();
        User GetById(string id); 
    }

    public class UserService:IUserService{

       private readonly AppSettings _appSettings;
       private readonly IMongoCollection<User> _user;

       public UserService(IPokedexDatabaseSettings settings, IOptions<AppSettings> appSettings){
           var client = new MongoClient(settings.ConnectionString);
           var database = client.GetDatabase(settings.DatabaseName);

           _user = database.GetCollection<User>(settings.UsersCollectionsName);
           _appSettings = appSettings.Value;
       }

       public User Authenticate(string login, string password){
           var user = _user.Find<User>(usr => usr.Login == login).FirstOrDefault();
            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            Update(user.Login,user);
            user.Password = null;

           return user;
       }

        public IEnumerable<User> GetAll(){
           List<User> users = _user.Find(user=>true).ToList();
             
            users.Select(u=>{u.Password = null;u.Token = null; return u;}).ToList();
            return users;
        }        

        public User GetById(string id) {
            var user = _user.Find<User>(usr => usr.Id == id).FirstOrDefault();

            if (user != null) 
                user.Password = null;

            return user;
        }

        public User Create(User user){
            _user.InsertOne(user);
            return user;
        }

        public void Update(string login, User userIn)=>_user.ReplaceOne(user=>user.Login == login,userIn);
        
        public void Remove(User userIn)=>_user.DeleteOne(user=>user.Login == userIn.Login);
        
        public void Remove(string login)=>_user.DeleteOne(user=>user.Login == login);
       
    }
}