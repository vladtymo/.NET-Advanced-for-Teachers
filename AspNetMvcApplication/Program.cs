using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DataAccess;
using Core.Interfaces;
using Core.Entities;
using AspNetMvcApplication.Helpers;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("LocalDb");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext(connStr);
builder.Services.AddIdentity();

builder.Services.AddRepositories();
builder.Services.AddAutoMapper();

// service configurations
builder.Services.AddCustomServices();

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// seed roles and admin user
using(var scope = app.Services.CreateScope())
{
    var provider = scope.ServiceProvider;

    Seeder.SeedRoles(provider).Wait();
    Seeder.SeedAdminUser(provider).Wait();
}

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
app.UseAuthentication();;

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
