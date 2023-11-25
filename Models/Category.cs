namespace NewsAndWeatherAPI.Models;

public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public List<LinkCategoryToNews> CategoriesToNews { get; set; }
}