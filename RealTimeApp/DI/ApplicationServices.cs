namespace RealTimeApp.DI;

public static class ApplicationServices
{
    public static void AddApplicationService(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.Configure<OptionToken>(configuration.GetSection("Jwt"));

        // Add services to the container.
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

        //builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Jwt:SecretPhrase").Value);

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IChatService, ChatService>();
    }
}