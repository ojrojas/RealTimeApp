namespace RealTimeApp.Dtos;

public record ListChatsResponse
{
    public IEnumerable<Chat> Chats;
}