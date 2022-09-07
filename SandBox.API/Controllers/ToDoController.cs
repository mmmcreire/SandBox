using Microsoft.AspNetCore.Mvc;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;

namespace SandBox.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IGetToDosHandler _getToDosHandler;
    private readonly IGetByIdHandler _getByIdHandler;
    private readonly ICreateToDoHandler _createToDoHandler;

    public ToDoController(
        IGetToDosHandler getToDosHandler,
        IGetByIdHandler getByIdHandler,
        ICreateToDoHandler createToDoHandler
        )
    {
        _getToDosHandler = getToDosHandler;
        _getByIdHandler = getByIdHandler;
        _createToDoHandler = createToDoHandler;

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
}