namespace CoreProject.Services;

public class DownloaderService : IDownloaderService
{
    private string _errorMessage = string.Empty;

    public bool DownloadImage(string url, string uploadPath, IProgress<int> progress, CancellationToken cancellationToken)
    {
        return DownloadImageAsync(url, uploadPath, progress, cancellationToken).Result;
    }

    public async Task<bool> DownloadImageAsync(string url, string uploadPath, IProgress<int> progress, CancellationToken cancellationToken)
    {
        _errorMessage = string.Empty;
        using var client = new HttpClient();

        try
        {
            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            using var stream = await response.Content.ReadAsStreamAsync();

            using var fileStream = new FileStream(uploadPath, FileMode.Create, FileAccess.Write, FileShare.None);

            var buffer = new byte[2048];
            int bytesRead;
            long totalBytesRead = 0;
            var contentLength = response.Content.Headers.ContentLength;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                totalBytesRead += bytesRead;

                progress.Report((int)(totalBytesRead * 100 / contentLength));
            }

            return true;
        }
        catch (Exception e)
        {
            _errorMessage = e.Message;
            return false;
        }
    }

    public string GetErrorMessage() => _errorMessage;
}
