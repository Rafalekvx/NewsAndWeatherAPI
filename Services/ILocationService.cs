using NewsAndWeather.Models;

namespace NewsAndWeatherAPI.Services;

public interface ILocationService
{
    List<Location> GetAll();
}