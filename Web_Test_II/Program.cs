using Web_Test_II_DAL;
using Web_Test_II_DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web_Test_II.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebTestDB>(options => options.UseSqlServer(connection), ServiceLifetime.Scoped);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/login");
        options.AccessDeniedPath = new PathString("/Account/Login");
    });
builder.Services.AddRepositoriesInDB();
builder.Services.RegistrationServices();
builder.Services.AddRazorPages();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
