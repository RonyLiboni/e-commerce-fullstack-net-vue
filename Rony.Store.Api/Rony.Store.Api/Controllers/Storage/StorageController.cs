using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Api.Controllers.Storage;

[ApiController]
[Route("storage")]
public class StorageController(IStorageService storageService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImageToTemporaryStorageAsync([Required] IFormFile file)
    {
        return Ok(new { fileKey = await storageService.UploadFileInTemporaryStorageAsync(file) });
    }

    [HttpGet]
    public IActionResult GetByFileKey([FromQuery] string fileKey)
    {
        var filePath = storageService.GetByFileKey(fileKey);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }

        return PhysicalFile(filePath, contentType);
    }
}