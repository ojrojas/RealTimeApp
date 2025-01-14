namespace RealTimeApp.Models;

public class Message
{
    public Guid Id { get; set; }
    public DateTimeOffset MessageDate { get; set; } = DateTime.UtcNow;
    public required string MessageWrited { get; set; }
    public bool IsReadMessage { get; set; } = false;
    public ComunicateType ComunicateType { get; set; }
    public Chat Chat { get; set; }
    public Guid ChatId { get; set; }
}


public enum ComunicateType
{
    Announcer = 1,
    Receiver
}