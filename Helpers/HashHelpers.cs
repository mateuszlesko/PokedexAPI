using System;
using System.Text;
using System.Security.Cryptography;

namespace PokeApi.Helpers{
    public static class HashHelpers{

        public static int ModularHashing(string data, int BIGPRIMENUMBER, int tableLength){
            int hashValue = data.GetHashCode();
            return ((hashValue & 0x7fffffff)%BIGPRIMENUMBER)%tableLength;
        }
        public static int ModularHashing(string data,int tableLength){
            return int.Parse(data,System.Globalization.NumberStyles.HexNumber) % tableLength;
        }
        public static string HashPassword(string password){
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  

        }
    }
}