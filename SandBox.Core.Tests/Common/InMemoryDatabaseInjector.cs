using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SandBox.Infra.Database;

namespace SandBox.Core.Tests.Common;

internal static class InMemoryDatabaseInjector
{
    internal static IServiceCollection InjectInMemoryDatabase(
        this IServiceCollection services
    ) => services
            .AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<DatabaseContext>(e => e.UseInMemoryDatabase(Guid.NewGuid().ToString()));
}