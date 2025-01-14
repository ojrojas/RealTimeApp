namespace RealTimeApp.Dtos;

public record UserInfoResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}