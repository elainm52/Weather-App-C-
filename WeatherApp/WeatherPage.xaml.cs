using WeatherApp.Services;
using WeatherApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public ObservableCollection<Models.List> HourlyWeatherList { get; set; }
    public ObservableCollection<Grouping<DateTime, Models.List>> FiveDayWeatherList { get; set; }

    public double latitude;
    public double longitude;

    public WeatherPage()
    {
        InitializeComponent();
        HourlyWeatherList = new ObservableCollection<Models.List>();
        FiveDayWeatherList = new ObservableCollection<Grouping<DateTime, Models.List>>();
        cvHourlyWeather.ItemsSource = HourlyWeatherList;
        cvFiveDayWeather.ItemsSource = FiveDayWeatherList;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await GetLocation();
            await GetWeatherDataByLocation(latitude, longitude);
        });
    }

    public async Task GetLocation()
    {
        try
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                latitude = location.Latitude;
                longitude = location.Longitude;
            }
            else
            {
                Console.WriteLine("Location not available.");
                await DisplayAlert("Error", "Unable to fetch location. Please enable location services.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Location error: {ex.Message}");
            await DisplayAlert("Error", "Unable to fetch location. Please try again.", "OK");
        }
    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var root = await ApiService.GetWeather(latitude, longitude);
        if (root != null && root.List != null)
        {
            // Populate hourly forecast
            var hourlyForecast = ApiService.GetHourlyForecast(root.List);
            HourlyWeatherList.Clear();
            foreach (var item in hourlyForecast)
            {
                HourlyWeatherList.Add(item);
            }

            // Populate 5-day forecast
            var fiveDayForecast = await ApiService.GetFiveDayForecast(latitude, longitude);
            if (fiveDayForecast != null)
            {
                FiveDayWeatherList.Clear();
                foreach (var day in fiveDayForecast)
                {
                    FiveDayWeatherList.Add(new Grouping<DateTime, Models.List>(day.Key, day.Value));
                }
            }
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "Search City", message: "Enter the city name:", placeholder: "City name", accept: "Search", cancel: "Cancel");
        Console.WriteLine($"User Input: {response}");
        if (!string.IsNullOrWhiteSpace(response))
        {
            await GetWeatherDataByCity(response.Trim());
        }
        else
        {
            await DisplayAlert("Error", "City name cannot be empty.", "OK");
        }
    }

    public async Task GetWeatherDataByCity(string city)
    {
        var root = await ApiService.GetWeatherByCity(city);
        if (root != null && root.List != null)
        {
            var forecast = await ApiService.GetFiveDayForecastByCity(city);
            if (forecast != null)
            {
                UpdateUI(forecast, root.City);
            }
        }
        else
        {
            Console.WriteLine("Weather data not available.");
            await DisplayAlert("Error", "Unable to fetch weather data for the specified city. Please try again.", "OK");
        }
    }

    public void UpdateUI(Dictionary<DateTime, List<Models.List>> forecast, City? city)
    {
        FiveDayWeatherList.Clear();

        foreach (var day in forecast)
        {
            var grouping = new Grouping<DateTime, Models.List>(day.Key, day.Value);
            FiveDayWeatherList.Add(grouping);
        }

        var firstDay = forecast.FirstOrDefault();
        var firstItem = firstDay.Value?.FirstOrDefault();

        if (firstItem != null)
        {
            lbCity.Text = city?.Name ?? string.Empty;
            lbWeatherDesc.Text = firstItem.Weather?[0].Description ?? string.Empty;
            lbTemperature.Text = firstItem.Main?.Temperature + "°C" ?? string.Empty;
            lbHumidity.Text = firstItem.Main?.Humidity + "%" ?? string.Empty;
            lbWind.Text = firstItem.Wind?.Speed + "km/h" ?? string.Empty;
            imgWeatherIcon.Source = firstItem.Weather?[0].CustomIcon ?? "default_icon.png";
        }
    }
}
public class Grouping<TKey, TItem> : List<TItem>
{
    public TKey Key { get; private set; }

    public Grouping(TKey key, IEnumerable<TItem> items) : base(items)
    {
        Key = key;
    }
}


