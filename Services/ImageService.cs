using System;
using System.IO;
using System.Threading.Tasks;

namespace PokeApi.Services{
    public static class ImageService{
        private static string Path = @"/home/leskers606/Documents/hoeannstadium/PokeApi/wwwroot/static/img/pokemons";
        public static async Task<byte[]> ReadImageAsync(string Name, string FileName){
            byte[] imageBytes = await File.ReadAllBytesAsync($"{Path}/{Name}/{FileName}");
            return imageBytes;
        }
    }
}