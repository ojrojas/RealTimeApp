namespace RealTimeApp.Hubs;

public interface IChatHub
{
    Task SendMessageAsync(string message);
    Task SendNotificationAsync(string title, string message);
    Task SendAllNotificationAsync(string title, string message);
    Task NotificationConnectionUserAsync();
}
