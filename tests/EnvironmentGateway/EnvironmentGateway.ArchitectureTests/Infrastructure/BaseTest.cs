using System.Reflection;
using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using EnvironmentGateway.Infrastructure;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EnvironmentGateway.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(EnvironmentGatewayDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}