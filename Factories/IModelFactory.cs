using System;
using System.Collections.Generic;
namespace PokeApi.Factories.Interfaces
{
    public interface IModelFactory<Model>
    {
        public abstract void Fill();
        public Boolean IsEmpty();
        public List<Model> GetAllElements();
        public Model GetElement(String id);
        public abstract void PutElement(Model model);
        public abstract void DeleteElement(String id);
    }
}