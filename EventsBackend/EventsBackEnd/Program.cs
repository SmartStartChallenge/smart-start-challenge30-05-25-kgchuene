using Microsoft.AspNetCore.Identity;
using EventsBackEnd.Data;
using EventsBackEnd.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ClassroomDb"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));


// Seed initial users
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    await SeedRolesAndUsers(roleManager, userManager);
}

app.Run();

async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Seed roles
    string[] roles = { "Admin", "Host", "Attendee" }; // Updated roles
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed admin user
    var admin = new ApplicationUser
    {
        UserName = "admin@test.com",
        Email = "admin@test.com",
        Role = "Admin"
    };
    await CreateUser(userManager, admin, "Admin@123", "Admin");

    // Seed host user (replaces Teacher)
    var host = new ApplicationUser
    {
        UserName = "host@test.com", // New Host user
        Email = "host@test.com",
        Role = "Host"
    };
    await CreateUser(userManager, host, "Host@123", "Host"); // New password and role

    // Seed attendee user (replaces Learner)
    var attendee = new ApplicationUser
    {
        UserName = "attendee@test.com", // New Attendee user
        Email = "attendee@test.com",
        Role = "Attendee"
    };
    await CreateUser(userManager, attendee, "Attendee@123", "Attendee"); // New password and role
}

async Task CreateUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string role)
{
    if (await userManager.FindByEmailAsync(user.Email) == null)
    {
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}