using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Api.Configurations;

public static class SwaggerConfiguration
{
    public static void AddServiceSwagger(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Carbon Emission API", 
                    Version = "v1", 
                    Description = "API que permite a las empresas registrar, actualizar y consultar sus emisiones de carbono."
                });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
}