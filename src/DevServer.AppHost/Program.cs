var builder = DistributedApplication.CreateBuilder(args);



var weatherApi = builder.AddProject<Projects.WeatherForecastApi>("weatherApi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("devServerClient", "../DevServerClient")
    .WithReference(weatherApi)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "DEV_SERVER_CLIENT_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


builder.AddProject<Projects.EnvironmentGatewayApi>("environmentgatewayapi");


await builder.Build().RunAsync();
