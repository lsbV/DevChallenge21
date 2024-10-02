using CallComponent;
using TopicComponent;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;
using SemanticAnalysisComponent;

namespace AppBuilder;

public static class Builder
{
    public static IServiceCollection AddTopicComponent(this IServiceCollection services)
    {
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<ITopicRepository, TopicRepository>();
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
        services.AddAutoMapper(typeof(TopicComponent.AutoMapperProfile).Assembly);
        services.AddAutoMapper(typeof(CallComponent.AutoMapperProfile).Assembly);
        services.AddAutoMapper(typeof(SemanticAnalysisComponent.AutoMapperProfile).Assembly);

        return services;
    }

    public static IServiceCollection AddCallComponent(this IServiceCollection services)
    {
        services.AddScoped<ICallRepository, CallRepository>();
        services.AddScoped<ICallService, CallService>();
        services.AddScoped<ITranscriptionService, ProxyTranscriptionService>();
        return services;
    }

    public static IServiceCollection AddSemanticAnalysisComponent(this IServiceCollection services)
    {
        services.AddSingleton<ISemanticAnalysisService, OllamaSemanticAnalysisService>();
        services.AddSingleton(new OllamaApiClient(new Uri("http://localhost:11434"))
        {
            SelectedModel = "llama3.1:latest",

        });
        services.AddSingleton<PromptProvider>();
        return services;
    }


}