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

    public async ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Create new message from user {request.Chat.UserAnnouncer}");
        ChatMessageResponse response = new();

        try
        {
            await _context.Chats.AddAsync(request.Chat);
            await _context.SaveChangesAsync();
            response.MessageSended = true;

            // use hubcontext signalr to notification receiver to update page chats
            await _hub.Clients.Client(request.Chat.Receiver.ToString()).SendMessageAsync(request.Chat.Message);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, CancellationToken cancellationToken)
    {
        // next feature pagination chats 
        _logger.LogInformation($"Get list chats message to user {userId}");
        ListChatMessageResponse response = new();
        response.Chats = await _context.Chats.Where(x => x.UserAnnouncer.Equals(userId)).ToListAsync();

        return response;
    }
}