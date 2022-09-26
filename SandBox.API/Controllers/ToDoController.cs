using Microsoft.AspNetCore.Mvc;
using SandBox.API.Common;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Delete;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
using SandBox.Core.ToDos.MarkAsDone;
using SandBox.Core.ToDos.PutInProgress;
using SandBox.Core.ToDos.UpdateDescription;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.API.Controllers;

public class ToDoController : ApiController
{
    private readonly IGetToDosHandler _getToDosHandler;
    private readonly IGetByIdHandler _getByIdHandler;
    private readonly ICreateToDoHandler _createToDoHandler;
    private readonly IDeleteTodoHandler _deleteTodoHandler;
    private readonly IMarkAsDoneHandler _markAsDoneHandler;
    private readonly IPutInProgressHandler _putInProgressHandler;
    private readonly IUpdateDescriptionHandler _updateDescriptionHandler;

    public ToDoController(
        IGetToDosHandler getToDosHandler,
        IGetByIdHandler getByIdHandler,
        ICreateToDoHandler createToDoHandler,
        IDeleteTodoHandler deleteTodoHandler,
        IMarkAsDoneHandler markAsDoneHandler,
        IPutInProgressHandler putInProgressHandler,
        IUpdateDescriptionHandler updateDescriptionHandler,
        IDomainValidator validator
    ) : base(validator)
    {
        _getToDosHandler = getToDosHandler;
        _getByIdHandler = getByIdHandler;
        _createToDoHandler = createToDoHandler;
        _deleteTodoHandler = deleteTodoHandler;
        _markAsDoneHandler = markAsDoneHandler;
        _putInProgressHandler = putInProgressHandler;
        _updateDescriptionHandler = updateDescriptionHandler;

    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
       Ok(await _getToDosHandler.Handle());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _getByIdHandler.Handle(id);
        return Response(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateToDoCommand command)
    {
        var result = await _createToDoHandler.Handle(command);
        return Response(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id) =>
        Response(await _deleteTodoHandler.Handle(id));


    [HttpPut("{id:guid}/MarkAsDone")]
    public async Task<IActionResult> MarkAsDone([FromRoute] Guid id) =>
        Response(await _markAsDoneHandler.Handle(id));

    [HttpPut("{id:guid}/PutInProgress")]
    public async Task<IActionResult> PutInProgress([FromRoute] Guid id) =>
        Response(await _putInProgressHandler.Handle(id));

    [HttpPut("{id:guid}/UpdateDescription")]
    public async Task<IActionResult> UpdateDescription([FromRoute] Guid id, [FromBody] string description) =>
        Response(await _updateDescriptionHandler.Handle(new UpdateDescriptionCommand(id, description)));
}