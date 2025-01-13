
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
        api.MapGet("/usersconnected", GetUserConnected);

        return api;
    }

    private static async Task<IResult> GetUserConnected(HttpContext context, IApplicationUserService service, CancellationToken cancellationToken)
    {
        return await service.GetUsersConnected(cancellationToken);
    }

    [Authorize]
    private static async Task<IResult> GetUserInfo(HttpContext context, IApplicationUserService service, CancellationToken cancellationToken)
    {
        var result =  context.User.Claims.FirstOrDefault().Value;
        return await service.GetUserByIdAsync(result, cancellationToken);
    }

    [Authorize]
    private static async Task<IResult> Create(
        HttpContext context,
        IApplicationUserService service,
        CreateUserRequest request,
        CancellationToken cancellationToken)
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