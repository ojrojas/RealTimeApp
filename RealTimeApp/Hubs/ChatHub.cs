
namespace RealTimeApp.Hubs;

public interface IChatHub
{
    Task SendMessageAsync(string message);
    Task SendNotificationAsync(string title, string message);
}

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    public async Task SendMessage(string userId, string message)
    {
        await Clients.Client(userId).SendMessageAsync(message);
    }

    public async Task SendNotification(string userId, string title, string message)
    {
        await Clients.Client(userId).SendNotificationAsync(title, message);
    }

    public override Task OnConnectedAsync()
    {
        UsersConnected.ConnectedIds.Add(Context.User.Claims.FirstOrDefault().Value);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        UsersConnected.ConnectedIds.Remove(Context.User.Claims.FirstOrDefault().Value);
        return base.OnDisconnectedAsync(exception);
    }
}

public static class UsersConnected
{
    public static HashSet<string> ConnectedIds = new HashSet<string>();
}