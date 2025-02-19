using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FindFlights;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }); // BASE ADDRESS
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7220") }); // HTTPS LOCALHOST
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5240") }); // HTTP LOCALHOST

await builder.Build().RunAsync();