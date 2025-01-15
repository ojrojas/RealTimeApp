namespace RealTimeApp.Models;

public class Message
{
    public Guid Id { get; set; }
    public DateTimeOffset MessageDate { get; set; } = DateTime.UtcNow;
    public required string MessageWrited { get; set; }
    public Guid UserId { get; set; }
    public bool IsReadMessage { get; set; } = false;
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
    public byte[]? Attachment { get; set; }
}