using Application.Client;
using Application.HttpClientService;
using Application.Users;
using Common.AppSettingsConfig;
using Common.Dtos;
using EF.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Security.Configurations;

var builder = WebApplication.CreateBuilder(args);

//Add context in memory
builder.Services.AddDbContext<DotNetDbContext>(options =>
options.UseInMemoryDatabase("DotNetDB"));

APIServiceConfiguration.RegisterServices(builder.Services);
SwaggerExtensions.ConfigureSwagger(builder.Services);
JwtExtensions.ConfigureAuthentication(builder.Services, builder.Configuration);

builder.Services.AddHttpClient();

builder.Services.Configure<General>(builder.Configuration.GetSection("General"));
builder.Services.AddScoped<IManageUser, ManageUser>();
builder.Services.AddScoped<IManageClient, ManageClient>();
// Add services to the container.

builder.Services.AddControllers();

//ADD FluentValidation
builder.Services.AddScoped<IValidator<UsersDto>, CreationUserValidation>();
builder.Services.AddScoped<IValidator<ValidateUserDto>, ValidateUserValidation>();
builder.Services.AddScoped<IValidator<JsonHolderDto>, JsonHolderValidation>();

builder.Services.AddValidatorsFromAssemblyContaining<CreationUserValidation>();

builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
