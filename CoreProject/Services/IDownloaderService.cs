namespace CoreProject.Services;

public interface IDownloaderService
{
    void DownloadImage(string url, string uploadPath, IProgress<int> progress, CancellationToken cancellationToken);
}
