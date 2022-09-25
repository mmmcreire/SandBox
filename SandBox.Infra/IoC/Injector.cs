using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SandBox.Core.Ports;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Delete;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
using SandBox.Core.ToDos.MarkAsDone;
using SandBox.Core.ToDos.PutInProgress;
using SandBox.Core.ToDos.UpdateDescription;
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
            .AddScoped<IDeleteTodoHandler, DeleteTodoHandler>()
            .AddScoped<IUpdateDescriptionHandler, UpdateDescriptionHandler>()
            .AddScoped<IPutInProgressHandler, PutInProgressHandler>()
            .AddScoped<IMarkAsDoneHandler, MarkAsDoneHandler>()
            .AddValidatorsFromAssemblyContaining<CreateToDoValidator>()
            .AddDbContext<DatabaseContext>(e =>
                e.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=Todos;User Id=postgres;Password=123321")
                    .ConfigureWarnings(x => x.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning))
                    .LogTo(
                        Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information,
                        DbContextLoggerOptions.DefaultWithLocalTime |
                        DbContextLoggerOptions.SingleLine)
                    .EnableSensitiveDataLogging());
}
