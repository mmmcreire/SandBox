using Microsoft.AspNetCore.Mvc;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;

namespace SandBox.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly IGetToDosHandler _getToDosHandler;
    private readonly IGetByIdHandler _getByIdHandler;

    public ToDoController(IGetToDosHandler getToDosHandler, IGetByIdHandler getByIdHandler)
    {
        _getToDosHandler = getToDosHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
       Ok(await _getToDosHandler.Handle());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var todo = await _getByIdHandler.Handle(id);
        return todo != null ? Ok(todo) : NotFound("Id not founded!");
    }
}