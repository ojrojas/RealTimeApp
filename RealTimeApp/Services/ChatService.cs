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
        _logger.LogInformation($"Create new message from users {request.Users.Select(x => x.ToString())}");
        ChatMessageResponse response = new();

        try
        {
            Chat chat = default;
            var existChat = await FindChat(request);
            if (existChat == null)
                chat = await SaveNewChat(request, announcer, cancellationToken);

            Message message = new()
            {
                MessageWrited = request.Message,
                MessageDate = DateTime.UtcNow,
                Chat = chat,
            };

            await _context.Messages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            response.MessageSended = true;

            // use hubcontext signalr to notification receiver to update page chats
            await _hub.Clients.Clients(request.Users.Select(x => x.ToString())).SendMessageAsync(request.Message);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task<Chat> FindChat(ChatMessageRequest request)
    {
        await Task.CompletedTask;
        return new Chat {Name = ""};
    }

    private async ValueTask<Chat> SaveNewChat(ChatMessageRequest request, Guid announcer, CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            Users = [],
            Name = request.Users.ToString(),
            ChatDate = DateTime.UtcNow
        };

        await _context.Chats.AddAsync(chat, cancellationToken);

        return chat;
    }

    public async ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, Guid receiverId, CancellationToken cancellationToken)
    {
        // next feature pagination chats 
        _logger.LogInformation($"Get list chats message between userid: {userId} and receiverId: ${receiverId}");
        ListChatMessageResponse response = new();
        var chat = await FindChat(new ChatMessageRequest { Users = [userId, receiverId], Message = "" });
        chat.Messages = await _context.Messages.Where(x => x.ChatId.Equals(chat.Id)).ToListAsync();
        response.Chat = chat;

        return response;
    }

    public async ValueTask<ListChatsResponse> ListChatsAsync(Guid userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get list chats by announcerid");
        ListChatsResponse response = new();
        response.Chats = await _context.Chats.Where(x => x.Users.Any(u=> u.Equals(userId))).ToListAsync();

        return response;
    }
}