using Application.Users;
using Common.AppSettingsConfig;
using Common.Dtos;
using EF.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Security.Configurations;

var builder = WebApplication.CreateBuilder(args);

//Add context in memory
builder.Services.AddDbContext<DotNetDbContext>(options =>
options.UseInMemoryDatabase("DotNetDB"));

APIServiceConfiguration.RegisterServices(builder.Services);
SwaggerExtensions.ConfigureSwagger(builder.Services);

builder.Services.Configure<General>(builder.Configuration.GetSection("General"));
builder.Services.AddScoped<IManageUser, ManageUser>();
// Add services to the container.

builder.Services.AddControllers();

//ADD FluentValidation
builder.Services.AddScoped<IValidator<UsersDto>, CreationUserValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<CreationUserValidation>();
builder.Services.AddFluentValidationAutoValidation();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(c =>
{
    c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
});

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
