using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
public interface IStorageService
{
    Task<string> UploadFileInTemporaryStorageAsync(IFormFile file);
    void MoveFileToLongTermStorage(string fileKey);
    void RemoveFile(string fileKey);
    bool IsFileInLongTermStorage(string fileKey);
    string GetByFileKey(string fileKey);
}
