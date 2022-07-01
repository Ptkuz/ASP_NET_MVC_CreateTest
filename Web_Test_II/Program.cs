using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web_Test_II_DAL;
using Web_Test_II_DAL.Context;
using System;
using Web_Test_II.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebTestDB>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddRepositoriesInDB();

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

app.Run();
