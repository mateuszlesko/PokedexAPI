using System.Collections.Generic; 
using PokeApi.Models;
 
namespace PokeApi.Services.Interfaces{
    public interface IUserService{
        User Authenticate(string login,string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id); 
        bool CreateUser(User user);
    }
 }