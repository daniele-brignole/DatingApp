using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUser(DataContext context){
            if(await context.Users.AnyAsync()) return;            //esiste già nel DB?
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");   //legge il seed

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);    //deserializza il json in una lista di utenti

            foreach(var user in users){
                using var hmac = new HMACSHA512(); //esegue la solita operazione di autenticazione

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$word"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            
            await context.SaveChangesAsync();
        }
    }
}