using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FindFlights;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5240") }); // localhost http: - 5240 ; https: 7220
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://findflightsapiserver.onrender.com") }); // Backend API server address

await builder.Build().RunAsync();