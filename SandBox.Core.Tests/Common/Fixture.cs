using Microsoft.Extensions.DependencyInjection;
using SandBox.Infra.Database;
using SandBox.Infra.IoC;
using SandBox.Infra.Settings;

namespace SandBox.Core.Tests.Common;

public class Fixture
{
    private ServiceProvider ServiceProvider { get; }

    public Fixture()
    {
        var services = new ServiceCollection();
        services
            .InjectInMemoryDatabase()
            .Inject(CreateAppSettings());

        ServiceProvider = services.BuildServiceProvider();
        CreateDatabase();
    }

    private static AppSettings CreateAppSettings() =>
        new()
        {
            ConnectionString = "InMemory"
        };

    private void CreateDatabase()
    {
        var databaseContext = Get<DatabaseContext>();
        databaseContext.Database.EnsureCreated();
    }

    public T Get<T>() where T : notnull =>
        ServiceProvider.GetRequiredService<T>();
}