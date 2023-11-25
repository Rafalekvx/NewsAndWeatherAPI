using NewsAndWeatherAPI.Entities;

namespace NewsAndWeatherAPI.Models;

public class Post
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public int CreatedById { get; set; }
    public List<LinkCategoryToNews> CategoriesToNews { get; set; }
}