namespace PokeApi.Models{

    public class PokedexDatabaseSettings:IPokedexDatabaseSettings{
        public string PokemonsCollectionsName {get;set;}
        public string AttacksCollectionsName {get;set;}
        public string ConnectionString {get;set;}
        public string UsersCollectionsName {get;set;}
        public string DatabaseName {get;set;}
    }

    public interface IPokedexDatabaseSettings{
         string PokemonsCollectionsName {get;set;}
         string AttacksCollectionsName {get;set;}
         string UsersCollectionsName {get;set;}
         string ConnectionString {get;set;}
         string DatabaseName {get;set;}
}
}