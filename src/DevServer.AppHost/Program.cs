var builder = DistributedApplication.CreateBuilder(args);



var productionGateway = builder.AddProject<Projects.EnvironmentGateway>("production-gateway");



var weatherApi = builder.AddProject<Projects.WeatherForecastApi>("weatherapi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("devServerClient", "../DevServerClient")
    .WithReference(weatherApi)
    .WithReference(productionGateway)
    //.WithReference(stagingGateway)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "DEV_SERVER_CLIENT_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


await builder.Build().RunAsync();
