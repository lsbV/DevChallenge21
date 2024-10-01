﻿using CategoryComponent;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppBuilder;

public static class Builder
{
    public static IServiceCollection AddCategoryComponent(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }

    public static IServiceCollection AddAppDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase(connectionString);
        });
        using var scope = services.BuildServiceProvider().CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        return services;
    }

    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CategoryComponent.AutoMapperProfile).Assembly);
        return services;
    }

}