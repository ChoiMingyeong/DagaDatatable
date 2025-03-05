using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DagaDatatable;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;
using DagaDatatable.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var dbUrl = builder.Configuration["DBApiUrl"];
if (true == string.IsNullOrEmpty(dbUrl))
{
    return;
}

builder.Services.AddAuthorizationCore(p =>
{
    p.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    p.FallbackPolicy = p.DefaultPolicy;
});
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<DBService>(p => new(new HttpClient { BaseAddress = new Uri(dbUrl) }));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
