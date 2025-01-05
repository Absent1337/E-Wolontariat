using E_Wolontariat.Enums;
using E_Wolontariat.Models;
using E_Wolontariat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Inicjalizacja ról
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Tworzenie ról
        await CreateRolesAsync(roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during role creation: {ex.Message}");
    }
}

//Inicjacja u¿ytkownika
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Tworzenie ról
        await CreateUserAsync(userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during role creation: {ex.Message}");
    }
}

app.Run();

async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
{
    // Dodanie roli Admin
    if (!await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
    }

    // Dodanie roli Volunteer
    if (!await roleManager.RoleExistsAsync(UserRoles.Volunteer.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Volunteer.ToString()));
    }

    // Dodanie roli Organization
    if (!await roleManager.RoleExistsAsync(UserRoles.Organization.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Organization.ToString()));
    }
}

async Task CreateUserAsync(UserManager<IdentityUser> userManager)
{
    string adminEmail = "admin@admin.com";
    string adminPassword = "Admin123!";
    string volunteerEmail = "wolontariusz@email.com";
    string volunteerPassword = "Wolontariat123!";
    string organizationEmail = "organizacja@email.com";
    string organizationPassword = "Organizacja123!";
    // Dodanie u¿ytkownika Admin
    if (await userManager.FindByNameAsync(adminEmail) == null)
    {
        var user = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
    }
    // Dodanie u¿ytkownika Volunteer
    if (await userManager.FindByNameAsync(volunteerEmail) == null)
    {
        var user = new IdentityUser { UserName = volunteerEmail, Email = volunteerEmail };
        await userManager.CreateAsync(user, volunteerPassword);
        await userManager.AddToRoleAsync(user, UserRoles.Volunteer.ToString());
    }
    // Dodanie u¿ytkownika Organization
    if (await userManager.FindByNameAsync(organizationEmail) == null)
    {
        var user = new IdentityUser { UserName = organizationEmail, Email = organizationEmail };
        await userManager.CreateAsync(user, organizationPassword);
        await userManager.AddToRoleAsync(user, UserRoles.Organization.ToString());
    }
}