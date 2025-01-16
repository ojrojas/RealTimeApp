namespace RealTimeApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{

    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    /// <summary>
    /// On model creating database, and specific change model
    /// </summary>
    /// <param name="builder">Model builder application</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Chat>()
        .HasMany(m => m.Messages)
        .WithOne(c => c.Chat)
        .HasForeignKey(f => f.ChatId)
        .IsRequired(false);

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
    }
}