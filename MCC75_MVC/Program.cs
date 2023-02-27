using MCC75_MVC.Contexts;
using MCC75_MVC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Context to Sql Server Database
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));

// Configure Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});

// Depedency Injection
builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<EducationRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<AccountRepository>();

// Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            //Usually, this is application base url
            ValidateAudience = false,
            //ValidAudience = builder.Configuration["JWT:Audience"],
            // If the JWT is created using web service, then this could be the consumer URL
            ValidateIssuer = false,
            //ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
// Menampilkan Status Code Error 401 atau 403
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
    {
        response.Redirect("/Unauthorized");
    }
    else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
    {
        response.Redirect("/Forbidden");
    }
});

app.UseSession();
// Memasang Token didalam Header
app.Use(async (context, next) =>
{
    var jwtoken = context.Session.GetString("jwtoken");
    if (!string.IsNullOrEmpty(jwtoken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + jwtoken);
    }
    await next();
});

// Middleware yang harus di tambahkan ketika menggunakan Authentication & Authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Status Code Error
/*app.UseStatusCodePagesWithRedirects("/errors/{0}");*/