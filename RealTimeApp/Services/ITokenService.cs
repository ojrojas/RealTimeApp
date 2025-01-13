namespace RealTimeApp.Services;

public interface ITokenService
{
    ValueTask<LoginResponse> GetTokenAsync(IdentityUser user);
}