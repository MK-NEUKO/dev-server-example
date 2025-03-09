using DevServer.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgresServer = builder.AddPostgres("postgresServer")
    .WithEnvironment("POSTGRES_DB", "DevServerDB")
    .WithPgAdmin();
var devServerDb = postgresServer.AddDatabase("DevServerDB");



builder.AddProject<Projects.EnvironmentGatewayApi>("EnvironmentGatewayApi")
    .WithScalar()
    .WithReference(devServerDb);

var weatherApi = builder.AddProject<Projects.WeatherForecastApi>("weatherApi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("devServerClient", "../DevServerClient")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "DEV_SERVER_CLIENT_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();




await builder.Build().RunAsync();
