using Microsoft.AspNetCore.Http;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;

namespace Rony.Store.Infrastructure.Storage;
public class StorageService : IStorageService
{
    private readonly string TEMPORARY_FOLDER_PATH = Path.Combine(Directory.GetCurrentDirectory(), "TemporaryFiles").Replace("\\Rony.Store.Api\\Rony.Store.Api\\", "\\Rony.Store.Api\\Rony.Store.Infrastructure\\Storage\\");
    private readonly string LONG_TERM_FOLDER_PATH = Path.Combine(Directory.GetCurrentDirectory(), "LongTermFiles");
    
    public async Task<string> UploadFileInTemporaryStorage(IFormFile file)
    {
        CreateFolderIfDoNotExist(TEMPORARY_FOLDER_PATH);
        string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public string MoveFileToLongTermStorage(string filePath)
    {
        CreateFolderIfDoNotExist(LONG_TERM_FOLDER_PATH);

        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        var fileName = Path.GetFileName(filePath);
        var longTermFilePath = Path.Combine(LONG_TERM_FOLDER_PATH, fileName);
        File.Move(filePath, longTermFilePath);
        return longTermFilePath;
    }

    public void RemoveFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private void CreateFolderIfDoNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}
