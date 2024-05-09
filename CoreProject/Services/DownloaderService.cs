namespace CoreProject.Services;

public class DownloaderService : IDownloaderService
{
    public bool DownloadImage(string url, string uploadPath, IProgress<int> progress, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        try
        {
            using HttpResponseMessage response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).Result;
            response.EnsureSuccessStatusCode();
            long? contentLength = response.Content.Headers.ContentLength;

            using var stream = response.Content.ReadAsStreamAsync().Result;

            byte[] image = new byte[(int)contentLength];
            byte[] buffer = new byte[64];
            int i = 0;
            int bytesRead;
            long totalBytesRead = 0;
            while ((bytesRead = stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).Result) > 0)
            {
                for (int j = 0; j < bytesRead; j++)
                {
                    var index = j + i * 1024;
                    if (index < (int)contentLength)
                    {
                        image[j + i * 1024] = buffer[j];
                    }
                    else
                    {
                        break;
                    }
                }
                i++;
                totalBytesRead += bytesRead;
                if (contentLength.HasValue)
                {
                    progress.Report((int)(totalBytesRead * 100 / contentLength));
                }
            }

            File.WriteAllBytes(uploadPath, image);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
