using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SandBox.Core.Ports;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
using SandBox.Infra.Database;
using SandBox.Infra.Repository;


namespace SandBox.Infra.IoC;

public static class Injector
{
    public static void Inject(this IServiceCollection services) =>
    services
        .AddScoped<IToDoRepository, ToDoRepository>()
        .AddScoped<ICreateToDoHandler, CreateToDoHandler>()
        .AddScoped<IGetToDosHandler, GetToDosHandler>()
        .AddScoped<IGetByIdHandler, GetByIdHandler>()
        .AddValidatorsFromAssemblyContaining<CreateToDoValidator>()
        .AddDbContext<DatabaseContext>(e =>
            e.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=todos;User Id=postgres;Password=123321")
                .ConfigureWarnings(x => x.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning))
                .LogTo(
                    Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information,
                    DbContextLoggerOptions.DefaultWithLocalTime |
                    DbContextLoggerOptions.SingleLine)
        .EnableSensitiveDataLogging());
}
