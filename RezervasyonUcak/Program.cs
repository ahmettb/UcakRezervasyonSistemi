using RezervasyonUcak.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RezervasyonUcak.Models.Token;

var builder = WebApplication.CreateBuilder(args);

//var jwtOptions= builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
//builder.Services.AddSingleton(jwtOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            ValidAudience = builder.Configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SigningKey"]))


        };
        opt.SaveToken = true;
        opt.RequireHttpsMetadata = false;
        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            }

        };


    }


    ).AddCookie(x=>x.Cookie.Name="token");

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITokenHandler, RezervasyonUcak.Models.Token.TokenHandler>();

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

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
