namespace RealTimeApp.Services;

public interface IChatService 
{
    ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request, Guid announcer, CancellationToken cancellationToken);
    ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, Guid receiverId, CancellationToken cancellationToken);
}