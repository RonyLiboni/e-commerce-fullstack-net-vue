using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.BaseServices;

namespace Rony.Store.Api.Controllers.BaseController;

[Controller]
public class BaseCreateReadUpdateController<Entity,Id,Form,DTO>(IBaseService<Entity, Id> baseService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> FindByIdAsync([FromRoute] Id id)
    {
        return Ok(await baseService.FindDTOByIdAsync<DTO>(id));
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync([FromBody] Form formDTO)
    {
        await baseService.CreateAsync(formDTO);
        return Created();
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateByIdAsync([FromRoute] Id id, [FromBody] Form formDTO)
    {
        await baseService.UpdateByIdAsync(formDTO, id);
        return Ok("Entity successfully updated.");
    }
}
