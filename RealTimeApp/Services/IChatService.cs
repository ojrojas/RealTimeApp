namespace RealTimeApp.Services;

public interface IChatService 
{
    ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request, CancellationToken cancellationToken);
    ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, CancellationToken cancellationToken);
}