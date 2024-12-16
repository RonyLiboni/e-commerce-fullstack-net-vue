using Microsoft.AspNetCore.Http;

namespace Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;
public interface IStorageService
{
    Task<string> UploadFileInTemporaryStorage(IFormFile file);
    void MoveFileToLongTermStorage(string fileKey);
    void RemoveFile(string fileKey);
    bool IsFileInLongTermStorage(string fileKey);
    string GetByFileKeyAsync(string fileKey);
}
