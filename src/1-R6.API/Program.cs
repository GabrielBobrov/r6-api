using System.Text;
using AutoMapper;
using EscNet.IoC.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using R6.API.Token;
using R6.Core.Communication.Handlers;
using R6.Core.Communication.Mediator;
using R6.Core.Communication.Mediator.Interfaces;
using R6.Core.Communication.Messages.Notifications;
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

#region Mediator

    builder.Services.AddMediatR(typeof(Program));
    builder.Services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
    builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

#endregion

#region Cryptography

    builder.Services.AddRijndaelCryptography(builder.Configuration["Cryptography:Key"]);
    
#endregion

#region Token

    builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

#endregion

#region Swagger

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "R6 API",
                    Version = "v1",
                    Description = "API Simulando funcionamento do jogo rainbow six siege.",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Bobrov",
                        Email = "gabrielbobrov@outlook.com.br",
                        Url = new Uri("https://github.com/GabrielBobrov")
                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor utilize Bearer <TOKEN>",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
                });
            });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
