using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PokeApi.Repositories.Interfaces
{
    public interface IModelRepository<Model>
    {
        public Boolean IsEmpty();
        public List<Model> GetAllElements();
        public Task<Model> GetElement(String id);
        public Task PutElement(Model model);
        public void DeleteElement(String id);
    }
}