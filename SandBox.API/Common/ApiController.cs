using Microsoft.AspNetCore.Mvc;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.API.Common;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly IDomainValidator _validator;

    public ApiController(IDomainValidator validator)
    {
        _validator = validator;
    }

    protected IActionResult Response(object result = null)
    {
        if (_validator.HasNotFound())
        {
            var message = _validator.GetNotFoundValidation();
            return NotFound(message);
        }

        if (_validator.HasFailValidation())
        {
            var content = _validator.GetFailValidations();
            return BadRequest(content);
        }

        return result is not null ? Ok(result) : Ok();
    }
}