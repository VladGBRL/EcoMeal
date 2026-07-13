using Microsoft.AspNetCore.Components.Authorization;
using EcoMeal.client.Services;
using EcoMeal.client.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddTransient<AuthenticationHeaderHandler>();
builder.Services.AddHttpClient("EcoMealApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7205/");
}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("EcoMealApi"));
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<PackageTypeService>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
