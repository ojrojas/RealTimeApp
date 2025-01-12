namespace RealTimeApp.Dtos;

public record LoginResponse
{
    public string TokenAccess { get; set; } = string.Empty;
    public long ExpireToken { get; set; }
}