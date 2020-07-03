namespace PokeApi.Entities{
    public static class Server{
        static int port = 5001;
        static string address = $"https://localhost:{port}/api/";

        public static string GetAddress()=>address;
        public static int GetPort()=>port;
    }
}