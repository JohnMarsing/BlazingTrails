using BlazingTrails.Client;
using BlazingTrails.Client.Features.Auth;
using BlazingTrails.Client.Features.Home;
using BlazingTrails.Client.State;
using Blazored.LocalStorage;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadContent>("head::after");

builder.Services.AddHttpClient("SecureAPIClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
}).AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AppState>();


var host = builder.Build(); // Added Logger which means I need the `host` variable
var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
builder.Logging.SetMinimumLevel(LogLevel.Warning);
logger.LogDebug("DEBUG Inside");
logger.LogInformation("INFO Inside *.Clients {Program}; after `var host = builder.Build()`", nameof(Program));
logger.LogWarning("Warning Inside");
logger.LogError("Error Inside");
await host.RunAsync(); //await builder.Build().RunAsync();


