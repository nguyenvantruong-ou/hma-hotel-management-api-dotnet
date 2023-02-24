using Hotel.API.Areas.Management.Services;
using Hotel.API.Areas.Management.Services.Interfaces;
using Hotel.API.DomainServices;
using Hotel.API.Utils;
using Hotel.Domain.Accounts.Repositories;
using Hotel.Domain.Feedbacks.Repositories;
using Hotel.Domain.Rooms.Repositories;
using Hotel.Domain.Services.Repositories;
using Hotel.Infrastructure.Data;
using Hotel.Infrastructure.Data.Accounts;
using Hotel.Infrastructure.Data.Rooms;
using Hotel.Infrastructure.Data.Services;
using Hotel.Infrastructure.Utils;
using Hotel.SharedKernel.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Hotel.Domain;
using System.Text;
using Hotel.Domain.Accounts.DomainServices.Interfaces;
using Hotel.Domain.Accounts.DomainServices;
using Hotel.API.Utils.Interfaces;
using Hotel.Domain.Statistics;
using Hotel.Infrastructure.Data.Statistics;
using Hotel.Domain.Rooms.DomainServices.Interfaces;
using Hotel.Domain.Rooms.DomainServices;
using Hotel.Domain.Orders.Repositories;
using Hotel.Infrastructure.Data.Orders;
using Hotel.Domain.Orders.DomainServices.Interfaces;
using Hotel.Domain.Orders.DomainServices;
using Hotel.SharedKernel.SMS;
using Hotel.Infrastructure.Data.Feedbacks;
using Hotel.Domain.Feedbacks.DomainServices.Interfaces;
using Hotel.Domain.Feedbacks.DomainServices;

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
builder.Services.AddScoped<UploadImage, CloudinaryUtil>();
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped<ISendSMSService, SendSMSService>();
builder.Services.AddScoped<ISMS, SMS>();

// Account
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IConvertToAccountService, ConvertToAccountService>();
builder.Services.AddScoped<ISendCodeService, SendCodeService>();
builder.Services.AddScoped<IImageManagementRepository, ImageManagementRepository>();
builder.Services.AddScoped<ITokenRegisterRepository, TokenRegisterRepository>();

// Service
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


// Room
builder.Services.AddScoped<IReadRoomService, ReadRoomService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IReadCommentService, ReadCommentService>();
builder.Services.AddScoped<ICreateCommentService, CreateCommentService>();
builder.Services.AddScoped<IDeleteCommentService, DeleteCommentService>();
builder.Services.AddScoped<IUpdateCommentService, UpdateCommentService>();


// Feedback
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

// Order
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRoomRepository, OrderRoomRepository>();
builder.Services.AddScoped<IOrderServiceRepository, OrderServiceRepository>();
builder.Services.AddScoped<ICoefficientRepository, CoefficientRepository>();
builder.Services.AddScoped<ICapitaRepository, CapitaRepository>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillService, BillService>();


// Order
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRoomRepository, OrderRoomRepository>();
builder.Services.AddScoped<IOrderServiceRepository, OrderServiceRepository>();
builder.Services.AddScoped<ICoefficientRepository, CoefficientRepository>();
builder.Services.AddScoped<ICapitaRepository, CapitaRepository>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillService, BillService>();


// Manage
builder.Services.AddScoped<IRoomManagementRepository, RoomManagementRepository>();
builder.Services.AddScoped<IRoomManagementService, RoomManagementService>();
builder.Services.AddScoped<IServiceManagementRepository, ServiceManagementRepository>();
builder.Services.AddScoped<IAccountManagementRepository, AccountManagementRepository>();
builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();
builder.Services.AddScoped<IStaffManagementRepository, StaffManagementRepository>();
builder.Services.AddScoped<IStaffTypeManagementRepository, StaffTypeManagementRepository>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IStatisticalRoomRepository, StatisticalRoomRepository>(); 
builder.Services.AddScoped<IStatisticalServiceRepository, StatisticalServiceRepository>(); 
builder.Services.AddScoped<IStatisticalOrderRepository, StatisticalOrderRepository>(); 



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
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
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
