using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using PokeApi.Models;

namespace PokeApi.Repositories{
    public class UserRepository : Interfaces.IUserRepository, Interfaces.IModelRepository<User>{
        private readonly IMongoCollection<User> userCollection;
        private IPokedexDatabaseSettings settings;

        public UserRepository(IPokedexDatabaseSettings settings){
            this.settings = settings;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            userCollection = database.GetCollection<User>(settings.PokemonsCollectionsName);
        }

        public async Task PutElement(User model)
        {
            await userCollection.InsertOneAsync(model); 
        }
        public void DeleteElement(String Id){
            userCollection.DeleteOne(poke=>poke.Id.Equals(Id));
        }
        public async Task<User> GetElement(String id) {
            User user = await userCollection.Find(poke => poke.Id == id).FirstOrDefaultAsync();
            if(user == null)
                return null;
            return user;
        }
        public async Task<User> GetElement(User user){
            return await userCollection.Find(usr => usr.Id == user.Id).FirstOrDefaultAsync();       
        }

        public async Task Update(string Id, User user ){
            await userCollection.ReplaceOneAsync(poke=>poke.Id == Id,user);
        }

        public async Task<List<User>> GetAllElements(){
            return await userCollection.Find(user => true).ToListAsync();
        }

        public async Task<List<User>> GetElementsCollection(IEnumerable<string> elementIds){
            List<User> elements = new List<User>();
            foreach(string id in elementIds){
                elements.Add(await GetElement(id));
                }
            return elements;
        }

        public async Task<User> Authorize(User user){
            User userAuth = await GetElement(user);
            return userAuth;
        }


    }
}