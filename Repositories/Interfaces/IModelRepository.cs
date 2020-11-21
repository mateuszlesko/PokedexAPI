using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PokeApi.Repositories.Interfaces
{
    public interface IModelRepository<Model>
    {
        Task<List<Model>> GetAllElements();
        Task<Model> GetElement(String id);
        Task PutElement(Model model);
        void DeleteElement(String id);
    }
}