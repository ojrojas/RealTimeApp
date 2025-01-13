var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.AddApplicationService();
var myCors = "mycors";
builder.Services.AddCors(setup => {
    setup.AddPolicy(myCors, policy => {
        policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});


builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(myCors);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapHub<ChatHub>("hub-connect");

app.AddAuthorizeEndpoints();
app.AddChatEndpoints();

app.Run();
