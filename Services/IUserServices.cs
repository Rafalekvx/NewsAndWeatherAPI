using Microsoft.AspNetCore.Mvc;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services
{
    public interface IUserServices
    {
        public string LoginUser(UserLoginDto login);
        public bool RegisterUser(UserRegisterDto register);
        public UserGetDto GetUserById(int id);
        public bool CheckTokenExpired(string token);
    }
}
