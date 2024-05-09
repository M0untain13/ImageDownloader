using System.Net;

namespace CoreProject.Services;

public class DownloaderService : IDownloaderService
{
    public string DownloadImage(string url, IProgress<int> progress, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        using HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).Result;

        // TODO: Нужно обрабатывать ситуации, когда путь неверный или статус кода не равен 200
        response.EnsureSuccessStatusCode();
        long? contentLength = response.Content.Headers.ContentLength;

        using var stream = response.Content.ReadAsStreamAsync().Result;

        byte[] buffer = new byte[1024];
        int bytesRead;
        long totalBytesRead = 0;
        while ((bytesRead = stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).Result) > 0)
        {
            totalBytesRead += bytesRead;
            if (contentLength.HasValue)
            {
                progress.Report((int)(totalBytesRead * 100 / contentLength));
            }
        }
        return "";
    }
}
