namespace RealTimeApp.Apis;

public static class ChatEndpoints
{
    public static RouteGroupBuilder AddChatEndpoints(this IEndpointRouteBuilder routerBuilder)
    {
        var api = routerBuilder.MapGroup(string.Empty);

        api.MapGet("/chat", ListChatMessages);
        api.MapPost("/chat", CreateChatMessage);

        return api;
    }

    [Authorize]
    private static async ValueTask<Results<Ok<ChatMessageResponse>, BadRequest<string>, ProblemHttpResult>> CreateChatMessage(IChatService service, ChatMessageRequest request, CancellationToken cancellationToken)
    {
        return TypedResults.Ok(await service.CreateMessageAsync(request, cancellationToken));
    }

    [Authorize]
    private static async ValueTask<Results<Ok<ListChatMessageResponse>, BadRequest<string>, ProblemHttpResult>> ListChatMessages(HttpContext context, IChatService service, CancellationToken cancellationToken)
    {
        var result = await context.AuthenticateAsync(IdentityConstants.ApplicationScheme);
        var userId = result.Principal.Claims.FirstOrDefault().Value;
        return TypedResults.Ok(await service.ListChatMessageAsync(Guid.Parse(userId), cancellationToken));
    }
}