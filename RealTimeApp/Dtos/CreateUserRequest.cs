namespace RealTimeApp.Dtos;

public record CreateUserRequest
{
    public required string Username { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirmed { get; set; }
    public required string RoleName { get; set; }
}