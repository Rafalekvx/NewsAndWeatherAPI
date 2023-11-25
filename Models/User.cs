namespace NewsAndWeatherAPI.Models;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Today;
    
    public int RoleID { get; set; }
    public virtual Role Role { get; set; }
}