namespace Rony.Store.WorkerService;
public class TemporaryStorageCleanUpJob
{
    public async Task Execute()
    {
        var temporaryFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "Rony.Store.Infrastructure\\Storage\\TemporaryFiles");
        if (Directory.Exists(temporaryFolderPath))
        {
            var directoryInfo = new DirectoryInfo(temporaryFolderPath);

            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.AddMinutes(-30))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error happened while deleting {file.FullName}: {ex.Message}");
                    }
                }
            }
        }
        await Task.CompletedTask;
    }
}