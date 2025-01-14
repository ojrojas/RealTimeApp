namespace RealTimeApp.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
}