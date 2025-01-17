namespace RealTimeApp.Services;

public class ChatService : IChatService
{
    private readonly ILogger<ChatService> _logger;
    private readonly IHubContext<ChatHub, IChatHub> _hub;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ChatService(ILogger<ChatService> logger, IHubContext<ChatHub, IChatHub> hub, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async ValueTask<ChatMessageResponse> CreateMessageAsync(ChatMessageRequest request, Guid userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Create new message from users {request.Users.Select(x => x.ToString())}");
        ChatMessageResponse response = new();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        try
        {
            Chat chat = default;
            var existChat = await FindChat(request);
            if (existChat == null)
                chat = await SaveNewChat(request, cancellationToken);

            Message message = new()
            {
                MessageWrited = request.Message,
                MessageDate = DateTime.UtcNow,
                Chat = chat ?? existChat,
                WriterUserId = userId,
                NameWriter = user.Name + " " + user.LastName
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
        if (request.ChatId != null)
            return await _context.Chats.FirstOrDefaultAsync(x => x.Id == request.ChatId);

        var result = from c in _context.Chats where c.Users.Any(x => x != null) select c;
        return await result.FirstOrDefaultAsync();
    }

    private async ValueTask<Chat> SaveNewChat(ChatMessageRequest request, CancellationToken cancellationToken)
    {
        Chat chat = new()
        {
            Users = request.Users.ToList(),
            Name = request.ChatId.ToString() ?? string.Empty
        };

        await _context.Chats.AddAsync(chat, cancellationToken);

        return chat;
    }

    public async ValueTask<ListChatMessageResponse> ListChatMessageAsync(Guid userId, CancellationToken cancellationToken)
    {
        // next feature pagination chats 
        _logger.LogInformation($"Get list chats message between userid: {userId}");
        ListChatMessageResponse response = new();
        var chat = await FindChat(new ChatMessageRequest { Users = [userId], Message = string.Empty });
        if (chat == null)
            return response;

        chat.Messages = await _context.Messages.Where(x => x.ChatId.Equals(chat.Id)).ToListAsync();
        response.Chat = chat;

        return response;
    }

    public async ValueTask<ListChatsResponse> ListChatsAsync(Guid userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get list chats by announcerid");
        ListChatsResponse response = new();
        response.Chats = await _context.Chats.Where(x => x.Users.Any(u => u.Equals(userId))).Include("Messages").ToListAsync();

        return response;
    }
}