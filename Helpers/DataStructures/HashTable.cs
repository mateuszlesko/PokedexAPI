using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public void PrintHashTable(){
            for(int i = 0; i < N; i++){
                Console.Write($"{i}: ");
                hashTable[i].PrintLinkedList();
            }
        }
        public void AddElement(Element element){
            string id = element.ToString();
            int length = id.Length;
            string key = $"{id[length-2]+id[length-1]}";
            int index = HashHelpers.ModularHashing(key,N);
            hashTable[index].AddToList(element);
        }

        public async ValueTask<Element> GetElement(string id){
            int length = id.Length;
            string key = $"{id[length-2]+id[length-1]}";
            int index = HashHelpers.ModularHashing(key,N);
            return await new ValueTask<Element>(hashTable[index].GetElement(id));
        }
    }
}