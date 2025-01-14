namespace RealTimeApp.Dtos;

public record ChatMessageRequest
{
    public Guid Receiver { get; set; }
    public required string Message { get; set; }
}