using AppBuilder;
using MainServer.Infrastructure.Filters;
using Microsoft.IdentityModel.Protocols.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<AppExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCategoryComponent();
builder.Services.AddAppDatabase(builder.Configuration.GetConnectionString("Database") ??
                                throw new InvalidConfigurationException("ConnectionString:Database"));
builder.Services.AddAppAutoMapper();
builder.Services.AddAutoMapper(typeof(MainServer.Infrastructure.AutoMapperProfiles.AutoMapperProfile).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
