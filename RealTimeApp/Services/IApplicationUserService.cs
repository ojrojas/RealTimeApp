using RealTimeApp.Dtos;

namespace RealTimeApp.Services;

public interface IApplicationUserService
{
    ValueTask<IResult> Login(LoginRequest request, CancellationToken cancellationToken);
    ValueTask<IResult> Create(CreateRequest request, CancellationToken cancellationToken);
    ValueTask<IResult> GetUserByIdAsync(string id, CancellationToken cancellationToken);
}