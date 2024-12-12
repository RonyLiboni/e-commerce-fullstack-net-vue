using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
public interface IStorageService
{
    Task<string> UploadFileInTemporaryStorage(IFormFile file);
    string MoveFileToLongTermStorage(string filePath);
    void RemoveFile(string filePath);
    bool IsFileInLongTermStorage(string imagePath);
}
