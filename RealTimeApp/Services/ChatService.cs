using System.Data.Common;

namespace RealTimeApp.Services;

public class ChatService : IChatService
{
    private readonly ILogger<ChatService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<ChatHub, IChatHub> _hub;

    public ChatService(ILogger<ChatService> logger, ApplicationDbContext context, IHubContext<ChatHub, IChatHub> hub)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _hub = hub ?? throw new ArgumentNullException(nameof(hub));
    }

    public async ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request, Guid announcer, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Create new message from user {request.Receiver}");
        ChatMessageResponse response = new();

        try
        {
            Chat chat = default;
            var existChat = await FindChat(request.Receiver, announcer);
            if (existChat == null)
                chat = await SaveNewChat(request, announcer, cancellationToken);

            Message message = new()
            {
                MessageWrited = request.Message,
                MessageDate = DateTime.UtcNow,
                Chat = chat,
                ComunicateType = ComunicateType.Announcer
            };

            await _context.Messages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            response.MessageSended = true;

            // use hubcontext signalr to notification receiver to update page chats
            await _hub.Clients.Client(request.Receiver.ToString()).SendMessageAsync(request.Message);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task<Chat> FindChat(Guid receiver, Guid announcer)
    {
        return await _context.Chats.FirstOrDefaultAsync(
            x => x.Announcer.Equals(announcer) && x.Receiver.Equals(receiver)
            || x.Announcer.Equals(receiver) && x.Receiver.Equals(announcer));
    }

    private async ValueTask<Chat> SaveNewChat(ChatMessageRequest request, Guid announcer, CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            Announcer = announcer,
            Receiver = request.Receiver,
            ChatDate = DateTime.UtcNow
        };

        await _context.Chats.AddAsync(chat);

        return chat;
    }

    public async ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, Guid receiverId, CancellationToken cancellationToken)
    {
        // next feature pagination chats 
        _logger.LogInformation($"Get list chats message between userid: {userId} and receiverId: ${receiverId}");
        ListChatMessageResponse response = new();
        response.Chat = await FindChat(receiverId, userId);
        response.Chat.Messages = await _context.Messages.Where(x=> x.ChatId.Equals(response.Chat.Id)).ToListAsync();        

        return response;
    }

    public ValueTask<ListChatsResponse> ListChatsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}