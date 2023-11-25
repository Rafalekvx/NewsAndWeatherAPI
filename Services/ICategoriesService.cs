using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public interface ICategoriesService
{
    List<CategoryGetDto> GetAll();
    
    CategoryGetDto GetById(int id);

}