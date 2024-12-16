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
        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        string filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
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

    private static void CreateFolderIfDoNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public bool IsFileInLongTermStorage(string imagePath)
    {
        return imagePath.Contains("\\Rony.Store.Api\\Rony.Store.Infrastructure\\Storage\\LongTermFiles");
    }

    private static string BuildFolderPath(string folderName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), folderName).Replace("\\Rony.Store.Api\\Rony.Store.Api\\", "\\Rony.Store.Api\\Rony.Store.Infrastructure\\Storage\\");
    }

    public string GetByFileKeyAsync(string fileKey)
    {
        var filePath = Path.Combine(LONG_TERM_FOLDER_PATH, fileKey);

        if (System.IO.File.Exists(filePath))
        {
            return filePath;
        }

        filePath = Path.Combine(TEMPORARY_FOLDER_PATH, fileKey);

        if (System.IO.File.Exists(filePath))
        {
            return filePath;
        }

        return null;
    }
}
