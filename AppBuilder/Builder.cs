using CallComponent;
using TopicComponent;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            options.UseSqlServer(connectionString);
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

    public static async Task<IServiceCollection> AddSemanticAnalysisComponentAsync(this IServiceCollection services)
    {
        services.AddSingleton<ISemanticAnalysisService, OllamaSemanticAnalysisService>();
        const string modelName = "llama3.1:latest";
        var ollama = new OllamaApiClient(new Uri("http://ollama:11434"))
        {
            SelectedModel = modelName,
        };
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<OllamaSemanticAnalysisService>>();
        var lastStep = 0;
        await foreach (var progress in ollama.PullModel(modelName))
        {
            if (progress is null || (int)progress.Percent % 10 != 0 || (int)progress.Percent == lastStep) continue;
            lastStep = (int)progress.Percent;
            logger.LogInformation("Downloading model {Percent}%", progress.Percent);
        }
        services.AddSingleton(ollama);
        services.AddSingleton<PromptProvider>();
        return services;
    }


}