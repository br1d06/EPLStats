using Microsoft.EntityFrameworkCore;
using WOD.WebUI.Data;
using WOD.WebUI.Services;
using WOD.WebUI.ViewModels;
using WOD.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEntityFrameworkNpgsql().
    AddDbContext<PostgresContext>(options => options.UseNpgsql
    (builder.Configuration.GetConnectionString("PostgresDbConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<FootballClubRepository>();
builder.Services.AddTransient<FootballClubService>();
builder.Services.AddTransient<NewsRepository>();
builder.Services.AddTransient<NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/");

app.Run();

