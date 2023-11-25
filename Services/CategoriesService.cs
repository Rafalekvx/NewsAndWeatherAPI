using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class CategoriesService : ICategoriesService
{
    private readonly NAWDBContext _dbContext;
    
    public CategoriesService(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CategoryGetDto GetById(int id)
    {
        try
        {
        Category getDataFromDb = _dbContext.Categories.FirstOrDefault(r => r.ID == id);
        CategoryGetDto category = new CategoryGetDto()
        {
            ID = getDataFromDb.ID,
            Name = getDataFromDb.Name
        };
        return category;
    }
    catch (Exception ex)
    {
        return null;
    }
    }

    public List<CategoryGetDto> GetAll()
    {
        try
        {
            List<Category> getDataFromDb = _dbContext.Categories.ToList();
            List<CategoryGetDto> listToReturn = new List<CategoryGetDto>();
            foreach (Category category in getDataFromDb)
            {
                listToReturn.Add(new CategoryGetDto()
                {
                    ID = category.ID,
                    Name = category.Name
                });

            }
            return listToReturn;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}