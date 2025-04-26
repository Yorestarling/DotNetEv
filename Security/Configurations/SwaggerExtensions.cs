using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Security.Configurations
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            var info = new OpenApiInfo()
            {

                Title = "DotNetEv",
                Version = "v1",
                Description = "API DOTNET EV",
                Contact = new OpenApiContact
                {
                    Name = "Yorestarling Mejia Beras",
                    Email = "yorestarlingdev@hotmail.com"
                }
            };
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(i =>
            {
                i.CustomSchemaIds(type => type.FullName);

                i.SwaggerDoc(info.Version, info);

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories);

                foreach (var xmlFile in xmlFiles)
                {
                    i.IncludeXmlComments(xmlFile);
                }

                i.AddSwaggerSecurity();
            });


        }


        /// <summary>
        /// Add Swagger Security by Bearer
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private static SwaggerGenOptions AddSwaggerSecurity(this SwaggerGenOptions options)
        {

            options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Favor colocar el JWT Token"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id="bearer"

                        }
                    },
                    new string[]{}
                }
            });
            return options;
        }
    }
}
