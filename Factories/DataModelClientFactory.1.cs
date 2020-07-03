using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PokeApi.Entities;
//using Newtonsoft.Json;

namespace PokeApi.Factories{
   
   public class DataModelClientFactory1<Model>{
        

       private readonly IHttpClientFactory _clientFactory;
       public IEnumerable<Model> Models {get; private set;}
       public bool GetModelError {get; private set;}

       public DataModelClientFactory1(IHttpClientFactory clientFactory){
           _clientFactory = clientFactory;
       }
       public async Task OnGet(){
           var request = new HttpRequestMessage(HttpMethod.Get,"https://localhost:5001/api/attacks/pokemon/5e557f1f152546e2ba09b4a7");
           request.Headers.Add("Accept","*/*");
           request.Headers.Add("User-Agent","DataModelClientFactory1");

           var client = _clientFactory.CreateClient();

           var response = await client.SendAsync(request);

           if(response.IsSuccessStatusCode){
               using var responseStream = await response.Content.ReadAsStreamAsync();
               Models = await JsonSerializer.DeserializeAsync<IEnumerable<Model>>(responseStream);
           }
           else{
               GetModelError = true;
               Models = Array.Empty<Model>();
           }
       }
        
       
    }
}