namespace RealTimeApp.Models;

public class Chat
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset ChatDate { get; set; } = DateTime.UtcNow;
    public ICollection<Guid> Users { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}
