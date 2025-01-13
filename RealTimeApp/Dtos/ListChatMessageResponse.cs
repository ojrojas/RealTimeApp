namespace RealTimeApp.Dtos;

public record ListChatMessageResponse
{
    public IEnumerable<Chat> Chats { get; set; }
}