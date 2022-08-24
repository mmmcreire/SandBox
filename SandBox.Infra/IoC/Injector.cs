using Microsoft.Extensions.DependencyInjection;
using SandBox.Core.Ports;
using SandBox.Core.ToDos.Get;
using SandBox.Infra.Repository;
using SandBox.Core.ToDos.GetById;

namespace SandBox.Infra.IoC
{
    public static class Injector
    {
        public static void Inject(this IServiceCollection services) =>
        services
            .AddScoped<IToDoRepository, ToDoRepository>()
            .AddScoped<IGetToDosHandler, GetToDosHandler>()
            .AddScoped<IGetByIdHandler, GetByIdHandler>();
    }
}
