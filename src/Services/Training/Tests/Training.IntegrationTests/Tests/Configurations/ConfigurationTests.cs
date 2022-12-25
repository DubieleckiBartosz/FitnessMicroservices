using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Training.IntegrationTests.Setup;

namespace Training.IntegrationTests.Tests.Configurations;

public class ConfigurationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly List<Type> _controllerTypes;
    private readonly WebApplicationFactory<Program> _factory;
    private MockServices? _servicesMock;

    public MockServices FakeServices()
    {
        _servicesMock ??= new MockServices();

        return _servicesMock;
    }

    public ConfigurationTests(WebApplicationFactory<Program> factory)
    {
        _controllerTypes = typeof(Program)
            .Assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
            .ToList();

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                MockServices.RegisterMockServices(services: ref services, FakeServices().GetMocks());

                _controllerTypes.ForEach(c => services.AddScoped(c));
            });
        });
    }
     
    [Fact]
    public void ConfigureServices_ForControllers_RegistersAllDependencies()
    {
        //Arrange
        var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory!.CreateScope();

        // Assert
        _controllerTypes.ForEach(t =>
        {
            var controller = scope.ServiceProvider.GetService(t); 
            Assert.NotNull(controller);
        });
    }
}