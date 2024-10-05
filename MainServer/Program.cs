using AppBuilder;
using MainServer.Infrastructure.Filters;
using Microsoft.IdentityModel.Protocols.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<AppExceptionFilter>();
});
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTopicComponent();
builder.Services.AddCallComponent();
await builder.Services.AddSemanticAnalysisComponentAsync();

builder.Services.AddAppDatabase(builder.Configuration.GetConnectionString("Database") ??
                                throw new InvalidConfigurationException("ConnectionString:Database"));
builder.Services.AddAppAutoMapper();
builder.Services.AddAutoMapper(typeof(MainServer.Infrastructure.AutoMapperProfiles.AutoMapperProfile).Assembly);

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHealthChecks(
    "/health");

await app.RunAsync();
