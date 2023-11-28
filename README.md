# NewsAndWeatherAPI

Welcome to the NewsAndWeatherAPI repository! This API is built using C# and ASP.NET MVC, providing the latest news and weather information based on user-selected locations. It also includes user authentication functionality, allowing users to register, log in, and obtain a JWT token for secure interactions.

## Overview

This API is an integral part of the NewsAndWeather application, available in a separate repository [here](https://github.com/Rafalekvx/NewsAndWeather). The NewsAndWeather application interacts with this API to retrieve news and weather information, providing a seamless experience to the users.

## Features

- **Latest News:** Retrieve the latest news articles from various sources.
- **Weather Information:** Get real-time weather updates for selected locations.
- **User Authentication:** Register a new account, log in, and obtain a JWT token for secure access.
- **Add New Information:** Use the JWT token to add new information to the system.

## Getting Started

To use the NewsAndWeatherAPI, follow these steps:

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Rafalekvx/NewsAndWeatherAPI
   cd NewsAndWeatherAPI
   ```
2. **Build and Run the Application:**
   - Open the solution file (`NewsAndWeatherAPI.sln`) in Visual Studio.
   - Configure the necessary settings such as database connection strings and JWT secrets in the `appsettings.json` file.
   - Build and run the application.

      `appsettings.json`
      ```json
      {
        "ConnectionStrings": {
          "LocalDB": "your_database_connection_string"
        },
        "JwtSettings": {
          "Secret": "your_jwt_secret",
          "Issuer": "your_issuer",
          "Audience": "your_audience"
        }
      }
      ```

   **This ConnectionStrings connection work only on debug in release change** `connectionStrings.json` **like below**
    ```json
      "ConnectionStrings": {
      "LocalDB": "your_database_connection_string",
      "ConnectedDB": "CONNECTION STRING TO YOUR DATABASE"
      }
      ```

   - Ensure your database is set up and accessible.

3. **API Endpoints:**
   - Latest News: `GET /api/news`
   - Locations to handle weather: `GET /api/locations`
   - User Registration: `POST /api/user/register`
   - User Login: `POST /api/user/login`
   - And more - use `swagger/index.html` in debug to see all endpoints

## Dependencies

- ASP.NET MVC: Web application framework
- Entity Framework: Object-relational mapper for database interaction
- JWT: JSON Web Token for user authentication
- Other dependencies can be found in the project file.

## Contributing

Contributions are welcome! Feel free to open issues and submit pull requests.

---

Thank you for using NewsAndWeatherAPI! If you have any questions or encounter issues, feel free to open an [issue](https://github.com/your-username/NewsAndWeatherAPI/issues). Happy coding!
