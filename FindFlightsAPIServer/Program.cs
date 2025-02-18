var builder = WebApplication.CreateBuilder(args);

// Wczytanie secrets.json i zmiennych œrodowiskowych
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Pobranie klucza API z konfiguracji
var apiKey = builder.Configuration["AviationStack:ApiKey"];
if (string.IsNullOrEmpty(apiKey))
{
    throw new InvalidOperationException("Brak API Key w secrets.json lub zmiennych œrodowiskowych!");
}

// Rejestracja API Key w DI
builder.Services.AddSingleton(new AviationStackSettings(apiKey));


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