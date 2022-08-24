using SandBox.Core.Ports;
using SandBox.Core.ToDos.GetById;

namespace SandBox.Core.ToDos.GetById
{
    public class GetByIdHandler : IGetByIdHandler
    {
        private readonly IToDoRepository _repository;

        public GetByIdHandler(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetByIdResult> Handle(Guid id)
        {
            var todo = await _repository.GetById(id);
            return todo == null ? null : new GetByIdResult(todo.Description, todo.Status);
        }
    }
}