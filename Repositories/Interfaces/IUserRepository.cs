using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokeApi.Models;

namespace PokeApi.Repositories.Interfaces{
     public interface IUserRepository{
        User Authenticate(string login,string password);
        IEnumerable<User> GetAll();
        User GetById(string id); 
    }
}