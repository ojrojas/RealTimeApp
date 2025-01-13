namespace RealTimeApp.Models;

public class Chat
{
    public Guid Id { get; set; }
    public Guid UserAnnouncer { get; set; }
    public required string NameAnnouncer { get; set; }
    public Guid Receiver { get; set; }
    public required string NameReceiver { get; set; }
    public DateTimeOffset MessageDate { get; set; } = DateTime.UtcNow;
    public required string Message { get; set; }
    public bool IsReadMessage { get; set; } = false;
    public ComunicateType ComunicateType {get;set;}
}

public enum ComunicateType
{
    Announcer =1,
    Receiver
}