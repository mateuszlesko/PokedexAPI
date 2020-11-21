using System;
using System.Threading.Tasks;
using PokeApi.Models;

namespace PokeApi.Repositories.Interfaces{
    public interface IUserRepository{
        Task<User> Authorize(User user);
    }
}