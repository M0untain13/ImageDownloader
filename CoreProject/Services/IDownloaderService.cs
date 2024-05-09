namespace CoreProject.Services;

public interface IDownloaderService
{
    string DownloadImage(string url, IProgress<int> progress, CancellationToken cancellationToken);
}
