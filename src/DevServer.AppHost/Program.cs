var builder = DistributedApplication.CreateBuilder(args);

var productionGateway = builder.AddProject<Projects.EnvironmentGateway>("productionGateway")
    .WithHttpEndpoint(port: 5500)
    .WithHttpsEndpoint(port: 7700)
    .WithExternalHttpEndpoints()
    .WithArgs("Production Gateway");
    


var stagingGateway = builder.AddProject<Projects.EnvironmentGateway>("stagingGateway")
    .WithHttpEndpoint(port: 5501)
    .WithHttpsEndpoint(port: 7701)
    .WithArgs("Staging Gateway");

var weatherApi = builder.AddProject<Projects.WeatherForecastApi>("weatherApi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("devServerClient", "../DevServerClient")
    .WithReference(weatherApi)
    .WithReference(productionGateway)
    .WithReference(stagingGateway)
    .WaitFor(weatherApi)
    .WithHttpEndpoint(env: "DEV_SERVER_CLIENT_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


await builder.Build().RunAsync();
