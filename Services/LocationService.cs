using NewsAndWeather.Models;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class LocationService : ILocationService
{
    private readonly NAWDBContext _dbContext;
    
    public LocationService(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    

    public List<Location> GetAll()
    {
        List<Location> listOfPosts = _dbContext.Locations.ToList();
        
        return listOfPosts;
    }
}