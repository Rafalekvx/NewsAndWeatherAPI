using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI;

public class NAWSeeder
{
    private readonly NAWDBContext _dbContext;
    public NAWSeeder(NAWDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                var Roles = GetRoles();
                _dbContext.Roles.AddRange(Roles);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                User user = new User() {Name = "Test" , Email = "Test@wp.pl", Password = "123", DateOfBirth = new DateTime(2001,10,4), RoleID =1};
                _dbContext.Add(user);
                _dbContext.SaveChanges();
            }
            
            
        }
    }
    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>()
        {
            new Role()
            {
                Name="Admin"
            },
            new Role()
            {
                Name="User"
            }
        };
        return roles;
    }
}