using Microsoft.AspNetCore.Identity;
using RealTimeApp.Dtos;

namespace RealTimeApp.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ApplicationUserService> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;

    public ApplicationUserService(
        UserManager<IdentityUser> userManager,
        ILogger<ApplicationUserService> logger,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async ValueTask<IResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("login user application request");
        var userApplication = await _userManager.FindByEmailAsync(request.Username);
        if (userApplication == null)
            return Results.Conflict("The username or password is not correct ");
        ArgumentNullException.ThrowIfNull(userApplication);
        var result = await _signInManager.PasswordSignInAsync(userApplication, request.Password, true, false);

        if (!result.Succeeded)
        {
            return Results.Conflict("The username or password is not correct ");
        }

        return Results.Ok(await _tokenService.GetTokenAsync(userApplication));
    }

    public async ValueTask<IResult> Create(CreateRequest request, CancellationToken cancellationToken)
    {

        if (!request.Password.Equals(request.PasswordConfirmed))
            return Results.BadRequest();

        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Username
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Results.BadRequest("Error create user");

        await _userManager.AddToRoleAsync(user, "Employed");

        return Results.Ok(result);
    }

    public async ValueTask<IResult> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        UserInfoResponse response = new();
        var user = await _userManager.FindByIdAsync(id);
        response.Email = user.Email;
        response.Id = user.Id;
        response.Name = user.UserName;

        return Results.Ok(response);
    }
}