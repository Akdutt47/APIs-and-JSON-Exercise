using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private const string ApiKey = "759208297a1100a8185d137b1015021e"; // Replace with your OpenWeatherMap API Key
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

    static async Task Main()
    {
        Console.Write("Enter city name: ");
        string city = Console.ReadLine();

        if (!string.IsNullOrEmpty(city))
        {
            await GetWeatherAsync(city);
        }
        else
        {
            Console.WriteLine("City name cannot be empty.");
        }
    }

    static async Task GetWeatherAsync(string city)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=imperial";
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                DisplayWeather(jsonResponse);
            }
            else
            {
                Console.WriteLine("Error fetching weather data. Please check the city name and try again.");
            }
        }
    }

    static void DisplayWeather(string json)
    {
        var data = JObject.Parse(json);
        string cityName = data["name"].ToString();
        string weather = data["weather"][0]["description"].ToString();
        double temperature = (double)data["main"]["temp"];
        double humidity = (double)data["main"]["humidity"];
        double windSpeed = (double)data["wind"]["speed"];

        Console.WriteLine("\n================ Weather Report ================");
        Console.WriteLine($"City: {cityName}");
        Console.WriteLine($"Condition: {weather}");
        Console.WriteLine($"Temperature: {temperature}°F");
        Console.WriteLine($"Humidity: {humidity}%");
        Console.WriteLine($"Wind Speed: {windSpeed} mph");
        Console.WriteLine("===============================================\n");
    }
}

