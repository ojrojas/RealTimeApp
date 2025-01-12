using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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
}