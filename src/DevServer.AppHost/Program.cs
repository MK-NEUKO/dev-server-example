var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WeatherApi>("weatherapi");

builder.Build().Run();
