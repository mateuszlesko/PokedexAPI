using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApi.Models;
using PokeApi.Helpers.DataStructures;
using PokeApi.Factories.Interfaces;
using System.Net.Http;
namespace PokeApi.Factories{

    public class PokemonFactory:IModelFactory<Pokemon>{
        
        BinarySearchTree<Pokemon> bstPokemon;
        List<Pokemon> pokemons;

        public PokemonFactory(List<Pokemon> _pokemons){
            pokemons = _pokemons;
            bstPokemon = new BinarySearchTree<Pokemon>();
            Fill();
        }

        public Boolean IsEmpty(){return pokemons == null;}

        public virtual void PutElement(Pokemon model){bstPokemon.Put(model.Id,model);}
        public virtual void DeleteElement(String id){bstPokemon.Delete(id);}
        public Pokemon GetElement(String id) {
            Pokemon pokemon = bstPokemon.Get(id);
            if(pokemon == null)
                return null;
            return pokemon;
        }

        public virtual void Fill(){
            if(!IsEmpty())
                foreach(Pokemon poke in pokemons)
                    bstPokemon.Put(poke.Id,poke);
        }

        public List<Pokemon> GetAllElements(){
            return pokemons;
        }
    }
}