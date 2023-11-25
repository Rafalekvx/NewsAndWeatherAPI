using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsAndWeatherAPI;
using NewsAndWeatherAPI.Entities;
using NewsAndWeatherAPI.Models;
using NewsAndWeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    var connectionString = builder.Configuration.GetConnectionString("LocalDB");
#else
    builder.Configuration.AddJsonFile("connectionStrings.json", optional: false, reloadOnChange: true);
    var connectionString = builder.Configuration.GetConnectionString("ConnectedDB");
#endif


// Add services to the container.
builder.Services.AddDbContext<NAWDBContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<NAWSeeder>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var authenticationSetting = new AuthenticationSettings();
builder.Services.AddSingleton(authenticationSetting);
builder.Configuration.GetSection("Authentication").Bind(authenticationSetting);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSetting.JwtIssuer,
        ValidAudience = authenticationSetting.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSetting.JwtKey)),
    };
});

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<NAWSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
