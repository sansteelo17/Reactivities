using Application.Activities;
using Application.Core;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        string corsPolicy)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Persistence"));
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy(corsPolicy, services =>
            {
                services
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders()
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .SetIsOriginAllowed(c => c.Contains(":3000"))
                .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
            });
        });

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Create>();

        return services;
    }
}