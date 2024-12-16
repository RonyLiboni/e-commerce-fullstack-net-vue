using Microsoft.AspNetCore.Http;
using Rony.Store.Domain.Contracts.Services.Infrastructure.Storage;

namespace Rony.Store.Infrastructure.Storage;
public class StorageService : IStorageService
{
    private readonly string TEMPORARY_FOLDER_PATH = BuildFolderPath("TemporaryFiles");
    private readonly string LONG_TERM_FOLDER_PATH = BuildFolderPath("LongTermFiles");

    public async Task<string> UploadFileInTemporaryStorage(IFormFile file)
    {
        CreateFolderIfDoNotExist(TEMPORARY_FOLDER_PATH);
        string fileKey = Guid.NewGuid() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileKey);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileKey;
    }

    public void MoveFileToLongTermStorage(string fileKey)
    {
        if (File.Exists(Path.Combine(TEMPORARY_FOLDER_PATH, fileKey)))
        {
            CreateFolderIfDoNotExist(LONG_TERM_FOLDER_PATH);
            var longTermFilePath = Path.Combine(LONG_TERM_FOLDER_PATH, fileKey);
            File.Move(Path.Combine(TEMPORARY_FOLDER_PATH, fileKey), longTermFilePath);
        }
    }

    public void RemoveFile(string fileKey)
    {
        var filePath = Path.Combine(LONG_TERM_FOLDER_PATH, fileKey);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileKey);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private static void CreateFolderIfDoNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public bool IsFileInLongTermStorage(string fileKey)
    {
        if (File.Exists(Path.Combine(LONG_TERM_FOLDER_PATH, fileKey)))
        {
            return true;
        }

        return false;
    }

    private static string BuildFolderPath(string folderName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), folderName).Replace("\\Rony.Store.Api\\Rony.Store.Api\\", "\\Rony.Store.Api\\Rony.Store.Infrastructure\\Storage\\");
    }

    public string GetByFileKeyAsync(string fileKey)
    {
        var filePath = Path.Combine(LONG_TERM_FOLDER_PATH, fileKey);

        if (File.Exists(filePath))
        {
            return filePath;
        }

        filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileKey);

        if (File.Exists(filePath))
        {
            return filePath;
        }

        return null;
    }
}
