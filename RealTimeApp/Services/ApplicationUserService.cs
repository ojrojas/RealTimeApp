namespace RealTimeApp.Services;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ApplicationUserService> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationUserService(
        UserManager<ApplicationUser> userManager,
        ILogger<ApplicationUserService> logger,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService,
        ApplicationDbContext context,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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
            return Results.Conflict("The username or password is not correct ");

        return Results.Ok(await _tokenService.GetTokenAsync(userApplication));
    }

    public async ValueTask<IResult> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create user request");
        if (!request.Password.Equals(request.PasswordConfirmed))
            return Results.BadRequest();

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Username,
            Name = request.Name,
            LastName = request.LastName,
        };

        var role = new IdentityRole
        {
            Name = request.RoleName
        };

        // do not use this in production
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return Results.BadRequest("Error create user");

        var roleExists = await _roleManager.FindByNameAsync(role.Name);

        if (roleExists == null)
            await _roleManager.CreateAsync(role);

        await _userManager.AddToRoleAsync(user, role.Name);
        _logger.LogInformation("User added to role request");

        return Results.Ok(result);
    }

    public async ValueTask<IResult> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get user info request");
        UserInfoResponse response = new();
        var user = await _userManager.FindByIdAsync(id);
        response.Email = user.Email;
        response.Id = user.Id;
        response.Name = user.Name;
        response.LastName = user.LastName;

        return Results.Ok(response);
    }

    public async ValueTask<IResult> GetUsersConnected(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get users connected");
        var users = from user in _context.Users
                    join
        uc in UsersConnected.ConnectedIds on user.Id equals uc
                    select new UserInfoResponse { Id = user.Id, Name = user.Name, LastName = user.LastName, Email = user.Email };

        if (!users.Any())
            return Results.NoContent();

        return Results.Ok(await users.ToListAsync());
    }
}