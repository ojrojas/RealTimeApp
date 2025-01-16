namespace RealTimeApp.Services;

public interface IChatService 
{
    ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request,Guid userId, CancellationToken cancellationToken);
    ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, CancellationToken cancellationToken);
    ValueTask<ListChatsResponse> ListChatsAsync(Guid userId, CancellationToken cancellationToken);
}