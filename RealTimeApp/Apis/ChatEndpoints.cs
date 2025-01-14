namespace RealTimeApp.Apis;

public static class ChatEndpoints
{
    public static RouteGroupBuilder AddChatEndpoints(this IEndpointRouteBuilder routerBuilder)
    {
        var api = routerBuilder.MapGroup(string.Empty);

        api.MapGet("/listchatmessages/{id:guid}", ListChatMessages);
        api.MapGet("/listchats", ListChats);

        api.MapPost("/chat", CreateChatMessage);

        return api;
    }

    [Authorize]
    private static async Task<IResult> ListChats(HttpContext context, IChatService service, CancellationToken cancellationToken)
    {
        var userId = context.User.Claims.FirstOrDefault().Value;
        return TypedResults.Ok(await service.ListChatsAsync(Guid.Parse(userId), cancellationToken));
    }

    [Authorize]
    private static async ValueTask<Results<Ok<ChatMessageResponse>, BadRequest<string>, ProblemHttpResult>> CreateChatMessage(
        HttpContext context,
        IChatService service,
        ChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var userId = context.User.Claims.FirstOrDefault().Value;
        return TypedResults.Ok(await service.CreateMessageAsync(request, Guid.Parse(userId), cancellationToken));
    }

    [Authorize]
    private static async ValueTask<Results<Ok<ListChatMessageResponse>, BadRequest<string>, ProblemHttpResult>> ListChatMessages(
        HttpContext context,
        Guid id,
        IChatService service,
        CancellationToken cancellationToken)
    {
        var userId = context.User.Claims.FirstOrDefault().Value;
        return TypedResults.Ok(await service.ListChatMessageAsync(Guid.Parse(userId), id, cancellationToken));
    }
}