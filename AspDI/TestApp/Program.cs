using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Task_2_TestApp;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<User> jsonUsers;
            List<RequestSettings> jsonFile;
            try
            {
                jsonFile = JsonSerializer.Deserialize<List<RequestSettings>>(await File.ReadAllTextAsync("Settings.json"));
                jsonUsers = JsonSerializer.Deserialize<List<User>>(await File.ReadAllTextAsync("RequestUsers.json"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
            HttpClient client = new HttpClient();
            var settingsBase = JsonSerializer.Deserialize<BaseAddressJson>(await File.ReadAllTextAsync("BaseAddress.json"));
            client.BaseAddress = new Uri(settingsBase.BaseAddress);

            foreach (var item in jsonUsers)
            {
                var jsonUser = JsonSerializer.Serialize(item);
                try
                {
                    var responseUser = await client.PostAsync("register", new StringContent(jsonUser, Encoding.UTF8, "application/json"));
                    var loginPass = await responseUser.Content.ReadAsStringAsync();
                    client.DefaultRequestHeaders.Add("Authorization", loginPass);
                    Console.WriteLine($"User with login {item.Login} and password {item.Password} is auth");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Login or pass is invalid");
                }
                
                
            }


            foreach (var item in jsonFile)
            {
                try
                {
                    Console.WriteLine($"Start work with {client.BaseAddress}{item.Link}");
                    await client.GetAsync(item.Link);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Wrong Link");
                    return;

                }

                var response = await client.GetAsync(item.Link);
                if (response.StatusCode.ToString() != item.StatusCode)
                {
                    Console.WriteLine(
                        $"Request for link {client.BaseAddress}{item.Link} return not expected response (expected {item.StatusCode} but found {response.StatusCode})");
                    continue;
                }

                Console.WriteLine($"{item.Link} return expected status code  {item.StatusCode}");
                Console.WriteLine();
                Console.WriteLine($"{ await response.Content.ReadAsStringAsync()}");
                Console.WriteLine();
                if (item.ExpectAnswer == null)
                {
                    continue;
                }

                Console.WriteLine(item.ExpectAnswer == await response.Content.ReadAsStringAsync()
                    ? $"Returned result is right, the body of request match with expected "
                    : $"Error with expected result (expect {item.ExpectAnswer} but take {await response.Content.ReadAsStringAsync()}");
                Console.WriteLine();


            }
            Console.WriteLine("Press any key to exit... ");
            Console.ReadKey();
        }
    }
}
