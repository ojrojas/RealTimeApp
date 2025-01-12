namespace RealTimeApp.Dtos;

public record CreateRequest 
{
    public required string Username {get;set;}
    public required string Name {get;set;}
    public required string Password {get;set;}
    public required string PasswordConfirmed {get;set;}
}