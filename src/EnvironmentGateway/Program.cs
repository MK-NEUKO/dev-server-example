using EnvironmentGateway;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var inMemoryConfig = new InMemoryConfig();
builder.Services.AddSingleton(inMemoryConfig);

builder.Services.AddReverseProxy()
    .LoadFromMemory(inMemoryConfig.Routes, inMemoryConfig.Clusters);

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapReverseProxy();

await app.RunAsync();
