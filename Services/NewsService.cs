using System.Security.Claims;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services;

public class NewsService : INewsService
{
    private readonly NAWDBContext _dbContext;
    
    public NewsService(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetByID(int id)
    {
        try
        {
            Post returnPost = _dbContext.Posts.FirstOrDefault(i => i.ID == id);
            returnPost.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.PostID == returnPost.ID).ToList();
            return returnPost;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public List<Post> GetAll()
    {
        try
        {
        List<Post> listOfPosts = _dbContext.Posts.ToList();
        foreach (var post in listOfPosts)
        {
            post.CategoriesToNews = _dbContext.CategoriesToNews.Where(r => r.PostID == post.ID).ToList();
        }
        return listOfPosts;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public bool AddNews(NewsAddDto news, int UserID)
    {
        try
        {
            List<LinkCategoryToNews> listOfCategories = new List<LinkCategoryToNews>();
            foreach (int category in news.Categories)
            {
                listOfCategories.Add(new LinkCategoryToNews()
                    { PostID = _dbContext.Posts.OrderBy(e => e.ID).Last().ID + 1, CategoryID = category });
            }

            if (listOfCategories.Count == 0)
            {
                listOfCategories.Add(new LinkCategoryToNews()
                    { PostID = _dbContext.Posts.OrderBy(e => e.ID).Last().ID + 1, CategoryID = 1 });
            }

            Post Added = new Post()
            {
                Title = news.Title,
                Description = news.Description,
                CategoriesToNews = listOfCategories,
                CreatedById = UserID,
                CreatedDate = DateTime.Now,
                ImageLink = news.ImageLink
            };

            _dbContext.Posts.Add(Added);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}