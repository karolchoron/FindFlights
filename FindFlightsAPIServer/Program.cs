var builder = WebApplication.CreateBuilder(args);

// Konfiguracja CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5228") // Adres aplikacji frontend HTTP
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// CORS
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
