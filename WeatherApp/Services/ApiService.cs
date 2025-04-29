using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    class ApiService
    {
        public static async Task<Root?> GetWeather(double latitude, double longitude)
        {
            var client = new HttpClient();
            var url = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid=204e8fa81c85119b7d48213d9aaab414";
            Console.WriteLine($"Request URL: {url}"); 
            try
            {
                var response = await client.GetStringAsync(url);
                Console.WriteLine(response); 
                return JsonConvert.DeserializeObject<Root>(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        public static async Task<Root?> GetWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid=204e8fa81c85119b7d48213d9aaab414";
            Console.WriteLine($"Request URL: {url}");
            try
            {
                var response = await httpClient.GetAsync(url);
                Console.WriteLine($"Response Status Code: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonResponse}");
                    return JsonConvert.DeserializeObject<Root>(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"API error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }



    }
}
