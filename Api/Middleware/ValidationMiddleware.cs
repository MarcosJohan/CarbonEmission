using System.Text.Json;
using Api.Models.Dtos;
using FluentValidation;

namespace Api.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IValidatorFactory validatorFactory)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();
            var body = await GetBody(context);
            context.Request.Body.Position = 0;
            
            var model = JsonSerializer.Deserialize(body, typeof(CarbonEmissionDto));
            var validator = validatorFactory.GetValidator(typeof(CarbonEmissionDto));
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(model));
                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(validationResult.ToDictionary());
                    return;
                }
            }
        }
        
        await next(context);
    }
    
    private async Task<string> GetBody(HttpContext context)
    {
        return await new StreamReader(context.Request.Body).ReadToEndAsync();
    }
}