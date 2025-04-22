using EnvironmentGateway.ArchitectureTests.Infrastructure;
using FluentAssertions;
using NetArchTest.Rules;

namespace EnvironmentGateway.ArchitectureTests.Layers;

public class LayersTests : BaseTest
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}