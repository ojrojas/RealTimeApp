namespace RealTimeApp.Services;

public class TokenService : ITokenService
{
    private readonly ILogger<TokenService> _logger;
    private readonly OptionToken _options;

    public TokenService(ILogger<TokenService> logger, IOptions<OptionToken> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Get Token jwt
    /// </summary>
    /// <param name="user">User login application</param>
    /// <returns>jwt string token</returns>
    public async ValueTask<LoginResponse> GetTokenAsync(IdentityUser user)
    {
        LoginResponse response = new();
        _logger.LogInformation("Generate token result");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_options.SecretPhrase);
        var claims = new List<Claim>()
        {
            new("sub", user.Id),
            new("email", user.Email),
            new("name", user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        await Task.CompletedTask;

        var expire =  DateTime.UtcNow.AddDays(7);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = expire,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        response.TokenAccess =  tokenHandler.WriteToken(token);
        response.ExpireToken = expire.Ticks;

        return response;
    }
}

