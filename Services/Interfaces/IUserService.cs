using System.Collections.Generic;
using PokeApi.Models;

namespace PokeApi.Services.Interfaces{
    public interface IUserService{
        User Authenticate(string login,string password);
        IEnumerable<User> GetAll();
        User GetById(string id); 
        void Update(string login, User userIn);
        void Remove(string login);
        void Remove(User userIn);
        User Create(User user);
    }
}