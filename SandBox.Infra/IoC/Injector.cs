using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SandBox.Core.Ports;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
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
        .AddValidatorsFromAssemblyContaining<CreateToDoValidator>();
}
