namespace RealTimeApp.Models;

public class Chat
{
    public Guid Id { get; set; }
    public Guid Announcer { get; set; }
    public Guid Receiver { get; set; }
    public DateTimeOffset ChatDate { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
}
