using Microsoft.AspNetCore.Mvc;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Api.Controllers.Storage;

[ApiController]
[Route("storage")]
public class StorageController(IStorageService storageService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImageToTemporaryStorage([Required] IFormFile file)
    {
        return Ok(new { filePath = await storageService.UploadFileInTemporaryStorage(file) });
    }
}