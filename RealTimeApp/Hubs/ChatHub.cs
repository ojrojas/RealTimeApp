namespace RealTimeApp.Hubs;

[Authorize]
public class ChatHub : Hub<IChatHub>
{
    /// <summary>
    /// Send Message
    /// </summary>
    /// <param name="receiverId">User receiver message</param>
    /// <param name="message">Message writed</param>
    public async Task SendMessage(string receiverId, string message)
    {
        await Clients.Client(receiverId).SendMessageAsync(message);
    }

    /// <summary>
    /// Send Notification 
    /// </summary>
    /// <param name="receiverId">User receiver message</param>
    /// <param name="title">Title message</param>
    /// <param name="message">Message writed</param>
    public async Task SendNotification(string receiverId, string title, string message)
    {
        await Clients.Client(receiverId).SendNotificationAsync(title, message);
    }

    public async Task NotificationConnectionUser()
    {
        await Clients.All.NotificationConnectionUserAsync();
    }

    public async Task SendAllNotification(string title, string message)
    {
        await Clients.All.SendAllNotificationAsync(title, message);
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