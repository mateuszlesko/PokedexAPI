using System;
using System.Collections.Generic;
using PokeApi.Helpers;
namespace PokeApi.Helpers.DataStructures{
    public class HashTable<Element>{
        
        int N;
        private LinkedList<Element>[] hashTable;
        
        public HashTable(int N){
            
            this.N = N;
            hashTable = new LinkedList<Element>[N];
            
            for(int i = 0; i < N; i++){
                hashTable[i] = new LinkedList<Element>();
            }
        }

        public void AddElement(Element element){
            int index = HashHelpers.ModularHashing(element.ToString(),313,N);
            hashTable[index].AddToList(element);
        }

        public Element GetElement(Element element){
            int index = HashHelpers.ModularHashing(element.ToString(),313,N);
            return hashTable[index].GetElement(element);
        }
    }
}