namespace eSchalt.Frontend.Classes.Tasks;

public class UploadCleanUpTask(ILogger<UploadCleanUpTask> logger) : BackgroundService
{
    private const string TempPath = "wwwroot/images/uploads/temp/";
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Task startet running at {time}", DateTimeOffset.Now);

            DeleteOldImages();

            // Clean up the images every hour
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
    
    private void DeleteOldImages()
    {
        string[] files = Directory.GetFiles(TempPath);

        foreach (string file in files)
        {
            var fileName = Path.GetFileName(file);
            if (fileName.Equals(".gitinclude", StringComparison.OrdinalIgnoreCase))
                continue;
            
            try
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.LastWriteTime < DateTime.Now.AddHours(-1))
                {
                    fileInfo.Delete();
                    Console.WriteLine($"Deleted file {fileInfo.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while trying to delete fiile {file}: {ex.Message}");
            }
        }
    }

}