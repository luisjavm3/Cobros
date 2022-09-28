using Cobros.API.Core.Business;
using Cobros.API.Core.Business.Interfaces;
using Cobros.API.Core.Helper;
using Cobros.API.DataAccess;
using Cobros.API.Middleware;
using Cobros.API.Repositories;
using Cobros.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var encodedKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
        ValidateLifetime = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Add Business layer
builder.Services.AddScoped<IAuthBusiness, AuthBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness >();
builder.Services.AddScoped<ICobroBusiness, CobroBusiness>();
builder.Services.AddScoped<ILoanBusiness, LoanBusiness>();

// Add UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Not-static helpers
builder.Services.AddScoped<IRefreshTokenHelper, RefreshTokenHelper>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cobros", Version = "v1" });
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Autenticación JWT (Bearer)",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }
                });

    // using System.Reflection;
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserMiddleware>();

app.MapControllers();

app.Run();
