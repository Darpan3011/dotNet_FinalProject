﻿using finalSubmission.Core.Domain.RepositoryContracts;
using finalSubmission.Core.ServiceContracts.IUserService;
using finalSubmission.Core.ServiceContracts;
using finalSubmission.Core.Services.UsersService;
using finalSubmission.Core.Services;
using finalSubmission.Infrastructure.DbContexts;
using finalSubmission.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ContactsManager.Core.Domain.IdentityEntities;
using System.Text;
using Microsoft.AspNetCore.Identity;
using finalSubmission.Infrastructure.Seeder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace finalSubmissionDotNet.BuilderExtensions
{
    public static class BuilderExtension
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer JWT_token' ",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
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


            services.AddDbContext<TaskOrderDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]!);
            });

            // Add Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TaskOrderDbContext>()
                .AddDefaultTokenProviders();

            // Configure JWT Authentication
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // Add role seeding
            services.AddScoped<RoleSeeder>();


            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGetAllTasks, GetAllTasks>();
            services.AddScoped<IChangeStatus, ChangeStatus>();
            services.AddScoped<ICreateTask, CreateTask>();
            services.AddScoped<IDeleteTask, DeleteTask>();
            services.AddScoped<IEditTask, EditTask>();
            services.AddScoped<IGetTaskByDueDate, GetTaskByDueDate>();
            services.AddScoped<IGetTaskByStatus, GetTaskByStatus>();
            services.AddScoped<IGetTaskByTitle, GetTaskByTitle>();
            services.AddScoped<IDeleteUser, DeleteUser>();
            services.AddScoped<ICreateUser, CreateUser>();
            services.AddScoped<IGetAllUsers, GetAllUsers>();
            services.AddScoped<IGetByUserID, GetByUserID>();

            // for app.UseHttpLogging();
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
                    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });


            return services;
        }
    }
}
