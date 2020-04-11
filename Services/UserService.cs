using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokeApi.Entities;
using PokeApi.Helpers;

namespace PokeApi.Services{
    
    public interface IUserService{
        User Authenticate(string login,string password);
        IEnumerable<User> GetAll();
        User GetById(int id); 
    }

    public class UserService:IUserService{
      
      private List<User> _users = new List<User>(){
           new User(){Id = 1, Login="Norman23", Password = "nr243ns",Role = Role.User},
           new User(){Id = 2, Login="Coco23", Password = "ere54!ns",Role = Role.Admin},
       };

       private readonly AppSettings _appSettings;

       public UserService(IOptions<AppSettings> appSettings){
           _appSettings = appSettings.Value;
       }

       public User Authenticate(string login, string password){
           var user = _users.SingleOrDefault(x=>x.Login == login && x.Password == password);

            if(user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

           return user;
       }
        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public User GetById(int id) {
            var user = _users.FirstOrDefault(x => x.Id == id);

            // return user without password
            if (user != null) 
                user.Password = null;

            return user;
        }
    }
}