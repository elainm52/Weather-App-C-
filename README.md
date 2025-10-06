# 🌤️ Weather App (C#)

A desktop weather application built in **C#** that lets users enter a city name and see the real-time weather conditions using an external weather API.

---

## 🧰 Built With / Tech Stack

- C# (using Windows Forms or WPF)  
- .NET Framework / .NET (whichever your project targets)  
- HTTP client (e.g. `HttpClient`) for API requests  
- JSON library (e.g. `Newtonsoft.Json`) for parsing responses  
- (Optional) Asynchronous calls (`async` / `await`)  
- (Optional) UI controls: text box, buttons, labels, etc.

---

## 🚀 Features

- Enter a **city name** and fetch its current weather  
- Display data such as **temperature**, **humidity**, **weather description**, etc.  
- Handle invalid city names or errors gracefully  
- (Optional) Switch units (Celsius / Fahrenheit)  
- (Optional) Minimal UI to keep it simple and clear  

---

## 📁 Project Structure (Suggested)

WeatherApp/
│
├─ WeatherApp.sln
├─ WeatherApp/
│ ├─ Program.cs
│ ├─ MainForm.cs ← the main window / form logic
│ ├─ MainForm.Designer.cs
│ ├─ WeatherService.cs ← handles API calls
│ ├─ Models/
│ │ └─ WeatherData.cs, etc.
│ └─ Utils/
│ └─ JsonHelper.cs, etc.
└─ .gitignore

yaml
Copy code

---

## ⚙️ Setup & Running the App

Follow these steps to run the app on your local machine:

### 1. Clone the repository  
```bash
git clone https://github.com/elainm52/Weather-App-C-.git
2. Open in Visual Studio
Open the .sln or project file in Visual Studio (or your preferred C# IDE).

3. Install required NuGet packages
You will likely need Newtonsoft.Json (or another JSON parsing library).
In Visual Studio, you can use:

text
Copy code
Install-Package Newtonsoft.Json
Or via the NuGet Package Manager UI.

4. Add your Weather API key
In your code (e.g. in WeatherService.cs), find where you call the API. There should be a placeholder or constant like:

csharp
Copy code
private const string ApiKey = "YOUR_API_KEY_HERE";
Replace "YOUR_API_KEY_HERE" with your actual key from a weather API provider (e.g. OpenWeatherMap).

You might also want to make it configurable (e.g. via appsettings.json or a config file) so you don’t hardcode it.

5. Build & Run
Compile and run the project (e.g. press F5).
The application window should open, letting you type a city name and get weather data.

🛠 Example Usage / UI Flow
Launch the app

Enter a city name (e.g. “Dublin”)

Press a “Get Weather” button

The app fetches data and shows:

Temperature

Humidity

Description (clear sky, rain, etc.)

(Optional) Additional info like wind, pressure

If city not found or API error, show a friendly error message

📦 Dependencies / Packages
Here are the main dependencies your project may use:

Package / Library	Purpose
Newtonsoft.Json	Parse JSON from the API response
System.Net.Http	Make HTTP requests (built-in)
(Optional) async / Task	For asynchronous calls

If you use any other libraries (for UI, icons, etc.), list them here too.

🎯 Error Handling & Edge Cases
If the user enters an invalid city name, show a message like “City not found.”

If there’s no internet or API error, show “Unable to fetch weather.”

Prevent empty input (e.g. if user presses “Get Weather” with blank text)

Use try/catch around network calls to avoid crashing

📈 Future Enhancements (To Do)
Show 5-day forecast rather than just current weather

Add unit toggle (Celsius / Fahrenheit)

Save recently searched cities

Use geolocation to get the user’s current city automatically

Improve UI (icons, nicer design, animations)

Support dark mode

Add unit tests for your service and logic

📌 License
This project is licensed under the MIT License (or another license you choose). See the LICENSE file for details.

🙋 Author
Your Name (Elain M.)
GitHub: elainm52
