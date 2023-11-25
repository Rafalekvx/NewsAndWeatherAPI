using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;

namespace NewsAndWeatherAPI.Services
{
    [Route("api/user")]
    public class UserServices : IUserServices
    {
        private readonly NAWDBContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserServices(NAWDBContext dbContext,IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public static long GetTokenExpirationTime(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
            var ticks = long.Parse(tokenExp);
            return ticks;
        }

        public bool CheckTokenExpired(string token)
        {
            try
            {
            var tokenTicks = GetTokenExpirationTime(token);
            var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;

            var now = DateTime.Now.ToUniversalTime();

            var valid = tokenDate > now;

            return valid;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public UserGetDto GetUserById([FromRoute]int id)
        {
            try
            {
                User user = _dbContext.Users.FirstOrDefault(u => u.ID == id);
                UserGetDto userDto = new UserGetDto()
                {
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                };
                if (user.RoleID == 2)
                {
                    userDto.Name = "*";
                    userDto.Email = "*";
                }
                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string LoginUser(UserLoginDto login)
        {
            try
            {
                User user = _dbContext.Users.Include(e => e.Role).FirstOrDefault(u => u.Email == login.Email);
                if (user is null)
                {
                    return "Invalid";
                }
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return "Invalid";
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Name}"),
                    new Claim(ClaimTypes.Email, $"{user.Email}"),
                    new Claim(ClaimTypes.DateOfBirth, $"{user.DateOfBirth}"),
                    new Claim(ClaimTypes.Role, $"{user.Role.Name}")
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
                var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                    _authenticationSettings.JwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: cred);
                var tokenHandler = new JwtSecurityTokenHandler();
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return tokenHandler.WriteToken(token);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RegisterUser(UserRegisterDto register)
        {
            try
            {
                if (_dbContext.Users.Any(e => e.Email == register.Email))
                {
                    return false;
                }
                User newUser = new User()
                {
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password,
                    RoleID = 1
                };
                var HashedPassword = _passwordHasher.HashPassword(newUser, newUser.Password);
                newUser.Password = HashedPassword;
                _dbContext.Add(newUser);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
