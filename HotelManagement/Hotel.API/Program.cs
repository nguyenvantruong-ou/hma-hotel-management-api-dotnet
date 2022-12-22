using Hotel.Domain.Accounts.Repository;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Accounts;
using Hotel.Infrastructure.Utils;
using Hotel.SharedKernel.Email;
using Microsoft.EntityFrameworkCore;
using NET.Domain;
using NET.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelManagementContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("WebApiDatabase")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITokenRegisterRepository, TokenRegisterRepository>();
builder.Services.AddScoped<IEmail, Email>();
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
             .SetIsOriginAllowed(_ => true)
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());


app.MapControllers();

app.Run();
