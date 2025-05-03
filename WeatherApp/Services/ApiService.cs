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
        private const string ApiKey = "204e8fa81c85119b7d48213d9aaab414";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        // Fetches weather data for a specific location using latitude and longitude.
        public static async Task<Root?> GetWeather(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}&units=metric&appid={ApiKey}";
            Console.WriteLine($"Request URL: {url}");
            try
            {
                var client = new HttpClient();
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
        // Fetches weather data for a specific city.
        public static async Task<Root?> GetWeatherByCity(string city)
        {
            var url = $"{BaseUrl}?q={city}&units=metric&appid={ApiKey}";
            Console.WriteLine($"Request URL: {url}");
            try
            {
                var client = new HttpClient();
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
        // Fetches and groups weather data by day for a specific location using latitude and longitude.
        public static async Task<Dictionary<DateTime, List<Models.List>>?> GetFiveDayForecast(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}&units=metric&appid={ApiKey}";
            Console.WriteLine($"Request URL: {url}");
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync(url);
                Console.WriteLine(response);
                var root = JsonConvert.DeserializeObject<Root>(response);

                if (root?.List == null)
                    return null;

                // Group data by day
                return GroupDataByDay(root.List);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        // Fetches and groups weather data by day for a specific city.
        public static async Task<Dictionary<DateTime, List<Models.List>>?> GetFiveDayForecastByCity(string city)
        {
            var url = $"{BaseUrl}?q={city}&units=metric&appid={ApiKey}";
            Console.WriteLine($"Request URL: {url}");
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync(url);
                Console.WriteLine(response);
                var root = JsonConvert.DeserializeObject<Root>(response);

                if (root?.List == null)
                    return null;

                // Group data by day
                return GroupDataByDay(root.List);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        // Groups weather data by day and includes 3-hour intervals.
        private static Dictionary<DateTime, List<Models.List>> GroupDataByDay(List<Models.List> data)
        {
            return data
                .Where(item => !string.IsNullOrEmpty(item.Dt_txt))
                .GroupBy(item => DateTime.Parse(item.Dt_txt!).Date)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        // Groups weather data for hourly forecast (next 24 hours).
        public static List<Models.List> GetHourlyForecast(List<Models.List> data)
        {
            return data
                .Where(item => !string.IsNullOrEmpty(item.Dt_txt))
                .Take(8) // Next 24 hours (3-hour intervals)
                .ToList();
        }


    }
}
