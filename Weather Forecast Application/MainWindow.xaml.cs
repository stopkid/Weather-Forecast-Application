using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Media;

namespace Weather_Forecast_Application
{
    public partial class MainWindow : Window
    {
        private const string ApiKey = "YOUR_API_KEY"; // Replace with your actual Visual Crossing API key

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CityTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CityTextBox.Text == "Enter city name...")
            {
                CityTextBox.Clear();
                CityTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void CityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CityTextBox.Text))
            {
                CityTextBox.Text = "Enter city name...";
                CityTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            if (string.IsNullOrEmpty(city) || city.Equals("Enter city name...", StringComparison.OrdinalIgnoreCase))
            {
                ShowErrorMessage("Please enter a valid city name.");
                return;
            }

            var weatherData = await GetWeatherDataAsync(city);
            if (weatherData?.CurrentConditions != null)
            {
                ShowWeatherInfo(city, weatherData.CurrentConditions);
            }
            else
            {
                ShowErrorMessage("Unable to fetch weather data. Please check the city name and try again.");
            }
        }

        private async Task<WeatherResponse?> GetWeatherDataAsync(string city)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string encodedCity = Uri.EscapeDataString(city); // URL encode the city name
                    string url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{encodedCity}?key={ApiKey}&unitGroup=metric";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<WeatherResponse?>(jsonResponse);
                    }
                    else
                    {
                        ShowErrorMessage("Error: " + response.ReasonPhrase);
                        return null;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                ShowErrorMessage($"Request error: {e.Message}");
                return null;
            }
            catch (JsonException e)
            {
                ShowErrorMessage($"JSON parsing error: {e.Message}");
                return null;
            }
        }

        private void ShowWeatherInfo(string city, CurrentConditions conditions)
        {
            if (conditions == null)
            {
                label1.Content = "Weather information is not available.";
                return;
            }

            string message = $"Temperature in {city}: {conditions.Temp}°C";
            label1.Content = message;
        }

        private void ShowErrorMessage(string message)
        {
            label1.Content = message;
        }
    }

    // Class to map the JSON response from Visual Crossing
    public class WeatherResponse
    {
        public CurrentConditions? CurrentConditions { get; set; }
    }

    // Class to represent the current weather data
    public class CurrentConditions
    {
        public float Temp { get; set; }
        public string? Condition { get; set; }
    }
}