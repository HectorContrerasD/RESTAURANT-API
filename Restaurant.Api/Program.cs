using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories;
using Restaurant.Api.Repositories.Abstractions;
using Restaurant.Api.Services;
using Restaurant.Api.Services.Abstractions;
using System.Runtime.CompilerServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddDatabase();
builder.AddAuth();
builder.AddRepositories();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x.WithOrigins(builder.Configuration.GetSection("AllowedDomains").Get<string[]>()!)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
public static class Extensions 
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RestaurantContext>(options =>
        {
            var connection = builder.Configuration["Database:Connection"];
            var version = builder.Configuration["Database:Version"];
            options.UseMySql(connection, ServerVersion.Parse(version));
        });
        return builder;
    }
    public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
        {
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var secret = builder.Configuration["Jwt:Secret"] ?? throw new ApplicationException("'Jwt:Secret' is a mandatory settings value");

            x.TokenValidationParameters = new()
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };
        });
        builder.Services.AddSingleton<IJwtService, JwtService>();
        return builder;
    }
    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ICategoriaRepository,CategoriaRepository>()
            .AddTransient<IMesaRepository,MesaRepository>()
            .AddTransient<IProductoRepository, ProductoRepository>()
            .AddTransient<ITicketRepository, TicketRepository>()
            .AddTransient<IVarianteRepository, VarianteRepository>();
        return builder;
    }
}