namespace RealTimeApp.Services;

public interface IApplicationUserService
{
    ValueTask<IResult> Login(LoginRequest request, CancellationToken cancellationToken);
    ValueTask<IResult> Create(CreateUserRequest request, CancellationToken cancellationToken);
    ValueTask<IResult> GetUserByIdAsync(string id, CancellationToken cancellationToken);
    ValueTask<IResult> GetUsersConnected(CancellationToken cancellationToken);
}