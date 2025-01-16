namespace RealTimeApp.Dtos;

public record ChatMessageRequest
{
    public Guid? ChatId { get; set; }
    public required IEnumerable<Guid> Users { get; set; }
    public required string Message { get; set; }
}