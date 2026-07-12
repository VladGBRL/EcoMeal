using EcoMeal.api.Entities;
using EcoMeal.api.Infrastructure;
using EcoMeal.api.Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EcoMealDbContext>(
    options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add Identity API endpoints with roles support
builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddRoles<IdentityRole<int>>()
.AddEntityFrameworkStores<EcoMealDbContext>();

builder.Services.AddAuthorization();

// Add CORS for Blazor site
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorSite", policy =>
    {
        policy.WithOrigins("http://localhost:5115", "https://localhost:7115")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "EcoMeal API");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazorSite");

app.UseAuthentication();
app.UseAuthorization();

// Map Identity default endpoints (/register, /login, /refresh, etc.)
app.MapIdentityApi<User>();

app.MapControllers();

// Seed roles at startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var roles = new[] { UserRoles.Admin, UserRoles.User };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }
    }
}

app.Run();
