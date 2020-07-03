using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PokeApi.Entities;
using Newtonsoft.Json;

namespace PokeApi.Factories{
   
   public class DataModelClientFactory<Model>{
        

        static string url = Server.GetAddress();
        //List<Model> data;
        static HttpClient client = new HttpClient();
        
        public DataModelClientFactory(){
            ConnectionConfig();
        }

        static  void ConnectionConfig(){
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

        }

        public async Task<List<Model>> GetModelDataAsync(string route){
            List<Model> data = new List<Model>();
            HttpResponseMessage response = await client.GetAsync(route);
            if(response.IsSuccessStatusCode){
                Console.WriteLine("Udalo sie");
               data = JsonConvert.DeserializeObject<List<Model>>(await response.Content.ReadAsStringAsync());
               // data = await response.Content.ReadAsAsyncStream<List<Model>>();
            }
            return data;
        }
        
       
    }
}