
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RealTimeApp.Dtos;
using RealTimeApp.Services;

namespace RealTimeApp.Apis;


public static class AuthorizeEndpoints
{
    public static RouteGroupBuilder AddAuthorizeEndpoints(this IEndpointRouteBuilder routerBuilder)
    {
        var api = routerBuilder.MapGroup(string.Empty);

        api.MapMethods("/connect/authorize", [HttpMethods.Get, HttpMethods.Post], AuthorizeAppication);
        api.MapPost("/connect/token", ConnectToken);
        api.MapMethods("/connect/logout", [HttpMethods.Get, HttpMethods.Post], LogoutApplication);
        api.MapPost("/connect/create", Create);
        api.MapGet("/connect/userinfo", GetUserInfo);

        return api;
    }

    [Authorize]
    private static async Task<IResult> GetUserInfo(HttpContext context, IApplicationUserService service, CancellationToken cancellationToken)
    {
        var result = await context.AuthenticateAsync(IdentityConstants.ApplicationScheme);
        return await service.GetUserByIdAsync(result.Principal.Claims.FirstOrDefault().Value, cancellationToken);
    }

    [Authorize]
    private static async Task<IResult> Create(HttpContext context, IApplicationUserService service, CreateRequest request, CancellationToken cancellationToken)
    {
        return await service.Create(request, cancellationToken);
    }

    private static async Task<IResult> LogoutApplication(HttpContext context)
    {
        Results.SignOut();
        await context.SignOutAsync(IdentityConstants.ApplicationScheme);
        return Results.Redirect("/");
    }

    private static async Task<IResult> ConnectToken(
        HttpContext context,
        IApplicationUserService service,
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        return await service.Login(request, cancellationToken);
    }

    [Authorize]
    private static async Task<string> AuthorizeAppication(HttpContext context)
    {
        await Task.CompletedTask;
        return "Is Authorized";
    }
}