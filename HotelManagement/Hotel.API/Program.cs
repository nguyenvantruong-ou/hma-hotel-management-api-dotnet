using Hotel.API.Areas.Management.Interfaces;
using Hotel.API.Areas.Management.Services;
using Hotel.API.Interfaces.Services;
using Hotel.API.Interfaces.Utils;
using Hotel.API.Services;
using Hotel.API.Utils;
using Hotel.Domain.Accounts.Repository;
using Hotel.Domain.Feedbacks.Repository;
using Hotel.Domain.Rooms.Repository;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Accounts;
using Hotel.Infrastructure.Data.Rooms;
using Hotel.Infrastructure.Utils;
using Hotel.SharedKernel.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NET.Domain;
using NET.Infrastructure.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelManagementContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("WebApiDatabase")));

// Utils
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddScoped<ICloudinary, CloudinaryUtil>();
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>)); 

//
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageManagementRepository, ImageManagementRepository>();

builder.Services.AddScoped<ITokenRegisterRepository, TokenRegisterRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();

// Manage
builder.Services.AddScoped<IRoomManagementRepository, RoomManagementRepository>();
builder.Services.AddScoped<IRoomManagementService, RoomManagementService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]))
        };

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(x => x
             .SetIsOriginAllowed(_ => true)
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
