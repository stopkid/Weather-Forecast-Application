# Weather Forecast Application

## Overview
The Weather Forecast Application is a WPF application that allows users to enter a city name and retrieve the current weather conditions using the Visual Crossing Weather API.

## Features
- User-friendly interface for entering city names.
- Fetches and displays current temperature and weather conditions.
- Handles errors gracefully, providing feedback to the user.

## Getting Started

### Prerequisites
- .NET Framework
- Visual Studio
- Newtonsoft.Json NuGet package

### Installation
1. Clone the repository or download the source code.
2. Open the solution in Visual Studio.
3. Replace `YOUR_API_KEY` in `MainWindow.xaml.cs` with your actual Visual Crossing API key.
4. Build and run the application.

## Code Structure

### MainWindow.xaml.cs
- **ApiKey**: Constant string to store the API key.
- **CityTextBox_GotFocus**: Clears the placeholder text when the text box gains focus.
- **CityTextBox_LostFocus**: Restores the placeholder text if the text box is empty when it loses focus.
- **GetWeatherButton_Click**: Event handler for the button click that retrieves weather data for the entered city.
- **GetWeatherDataAsync**: Asynchronous method that fetches weather data from the Visual Crossing API.
- **ShowWeatherInfo**: Displays the current weather information in the UI.
- **ShowErrorMessage**: Displays error messages in the UI.

### WeatherResponse Class
- Maps the JSON response from the Visual Crossing API.
- Contains a property for `CurrentConditions`.

### CurrentConditions Class
- Represents the current weather data.
- Contains properties for temperature and weather condition.

## Usage
1. Launch the application.
2. Enter a city name in the text box.
3. Click the "Get Weather" button to retrieve the weather information.

## Error Handling
The application provides user feedback for:
- Invalid city names.
- API request errors.
- JSON parsing errors.

## License
This project is licensed under the MIT License.
