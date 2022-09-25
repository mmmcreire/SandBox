using Microsoft.AspNetCore.Mvc;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Delete;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
using SandBox.Core.ToDos.MarkAsDone;
using SandBox.Core.ToDos.PutInProgress;
using SandBox.Core.ToDos.UpdateDescription;

namespace SandBox.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
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
        IDeleteTodoHandler deleteTodoHandler
        )
    {
        _getToDosHandler = getToDosHandler;
        _getByIdHandler = getByIdHandler;
        _createToDoHandler = createToDoHandler;
        _deleteTodoHandler = deleteTodoHandler;

    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
       Ok(await _getToDosHandler.Handle());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var todo = await _getByIdHandler.Handle(id);
        return todo != null ? Ok(todo) : NotFound("Id not found!");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateToDoCommand command)
    {
        var (result, errors) = await _createToDoHandler.Handle(command);
        if(errors != null)
            return BadRequest(errors);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _deleteTodoHandler.Handle(id);
        return Ok(result);
    }

    [HttpPut("MarkAsDone")]
    public async Task<IActionResult> MarkAsDone([FromRoute] Guid id)
    {
        var result = await _markAsDoneHandler.Handle(id);
        return Ok(result);
    }

    [HttpPut("PutInProgress")]
    public async Task<IActionResult> PutInProgress([FromRoute] Guid id)
    {
        var result = await _putInProgressHandler.Handle(id);
        return Ok(result);
    }

    [HttpPut("UpdateDescription")]
    public async Task<IActionResult> UpdateDescription([FromRoute] Guid id, [FromBody] string description)
    {
        var result = await _updateDescriptionHandler.Handle(id, description);
        return Ok(result);
    }
}