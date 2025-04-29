using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WeatherApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Coord? Coord { get; set; }
        public string? Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class List
    {
        public int Dt { get; set; }
        public string DateTime => UtcTimeLibrary.UtcTimeStamp.ConvertToUtc(Dt);

        public Main? Main { get; set; }
        public List<Weather>? Weather { get; set; }
        public Clouds? Clouds { get; set; }
        public Wind? Wind { get; set; }
        public Sys? Sys { get; set; }
        public string? Dt_txt { get; set; }
        public Snow? Snow { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Temperature => Math.Round(Temp);
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Sea_level { get; set; }
        public int Grnd_level { get; set; }
        public int Humidity { get; set; }
        public double Temp_kf { get; set; }
    }

    public class Root
    {
        [JsonProperty("cod")]
        public string? Cod { get; set; }

        [JsonProperty("message")]
        public int Message { get; set; }

        [JsonProperty("cnt")]
        public int Cnt { get; set; }

        [JsonProperty("list")]
        public List<List>? List { get; set; }

        [JsonProperty("city")]
        public City? City { get; set; }
    }
    public class Snow
    {
        [JsonProperty("3h")]
        public double _3h { get; set; }
    }

    public class Sys
    {
        public string? Pod { get; set; }
    }

    public class Weather
    {
        public int ID { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        //public string fullIconUrl => string.Format("https://openweathermap.org/img/wn/{0}@2x.png", icon);
        public string CustomIcon => $"icon_{Icon}.png";

    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

}
