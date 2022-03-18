using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using R6.Domain.Entities;
using R6.Infra.Context;
using R6.Infra.Interfaces;
using R6.Infra.Repositories;
using R6.Services.DTO;
using R6.Services.Interfaces;
using R6.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Jwt


    var secretKey = builder.Configuration["Jwt:Key"];

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

#region AutoMapper

    var autoMapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Operator, OperatorDto>().ReverseMap();
    });

            builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion


#region Services
    builder.Services.AddSingleton(d => builder.Configuration);
    builder.Services.AddScoped<IOperatorService, OperatorService>();
    builder.Services.AddScoped<IOperatorRepository, OperatorRepository>();
#endregion

#region Database
    builder.Services.AddDbContext<R6Context>(options => options.UseNpgsql(builder.Configuration["ConnectionStrings:R6APIPostgreSQL"]));
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
