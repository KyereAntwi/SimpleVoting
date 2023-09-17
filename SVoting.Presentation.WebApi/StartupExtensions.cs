﻿using Microsoft.OpenApi.Models;
using SVoting.Application;
using SVoting.Identity;
using SVoting.Persistence;
using SVoting.Presentation.WebApi.Middlewares;
using SVoting.Presentation.WebApi.Utility;

namespace SVoting.Presentation.WebApi;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);

        builder.Services.AddApplicationService();
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        builder.Services.AddControllers()
            .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SVoting Online System API");
            });
        }

        app.UseHttpsRedirection();

        //app.UseRouting();

        app.UseAuthentication();

        app.UseCustomExceptionHandler();

        app.UseCors("Open");

        app.UseAuthorization();

        app.MapControllers();

        return app;

    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "SVoting Online System API",

            });

            c.OperationFilter<FileResultContentTypeOperationFilter>();
        });
    }
}
