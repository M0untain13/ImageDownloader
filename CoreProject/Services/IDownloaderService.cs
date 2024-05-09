namespace CoreProject.Services;

public interface IDownloaderService
{
    bool DownloadImage(string url, string uploadPath, IProgress<int> progress, CancellationToken cancellationToken);
    string GetErrorMessage();
}
