using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FindFlights;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7220") }); // Ustawianie BaseAddress na backend API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5240") });


await builder.Build().RunAsync();