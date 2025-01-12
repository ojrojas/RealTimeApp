using Microsoft.AspNetCore.Identity;
using RealTimeApp.Dtos;

namespace RealTimeApp.Services;

public interface ITokenService
{
    ValueTask<LoginResponse> GetTokenAsync(IdentityUser user);
}