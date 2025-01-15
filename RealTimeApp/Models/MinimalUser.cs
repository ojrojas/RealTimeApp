namespace RealTimeApp.Models;

public class MinimalUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public Chat Chat { get; set; }
    public Guid ChatId { get; set; }
}