using System;
using System.Collections.Generic;
using System.Linq;
using PokeApi.Models;
using PokeApi.Helpers.DataStructures;
using PokeApi.Factories.Interfaces;
namespace PokeApi.Factories{

    public class AttackFactory:IModelFactory<Attack>{
        BinarySearchTree<Attack> bstAttack;
        List<Attack> attacks;
        public AttackFactory(List<Attack> attacks){
            this.attacks = attacks;
            bstAttack = new BinarySearchTree<Attack>();
            Fill();
        }
        public Boolean IsEmpty(){return attacks == null;}
        public virtual void Fill(){
                if(!IsEmpty())
                    foreach(Attack attack in attacks)
                        bstAttack.Put(attack.Id,attack);
        }
        public List<Attack> GetAllElements(){
                return attacks;
        }
        public virtual void PutElement(Attack model){bstAttack.Put(model.Id,model);}
            public virtual void DeleteElement(String id){bstAttack.Delete(id);}
            public Attack GetElement(String id) {
                Attack attack = bstAttack.Get(id);
                if(attack == null)
                    return null;
                return attack;
            }
    }
}