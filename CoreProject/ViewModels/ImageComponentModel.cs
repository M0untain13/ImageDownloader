using CoreProject.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace CoreProject.ViewModels;

public class ImageComponentModel : MvxViewModel
{
    public int Id { get; private set; }

    private readonly string _imageDir;

	private string _url;
	public string Url
	{
		get => _url;
		set => SetProperty(ref _url, value);
	}

	private string _localPath;
    public string LocalPath
    {
        get => _localPath;
        set => SetProperty(ref _localPath, value);
    }

    private int _localProgress;
    public int LocalProgress
    {
        get => _localProgress;
        set => SetProperty(ref _localProgress, value);
    }

    private string _status;
    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    private CancellationTokenSource? _cancellationTokenSource;

    public IMvxAsyncCommand StartDownloadCommand { get; }

	public IMvxAsyncCommand StopDownloadCommand { get; }

    public ImageComponentModel(IDownloaderService downloaderService, int id = -1)
	{
        Id = id;
        Url = string.Empty;
        _imageDir = "images";
        LocalPath = @$"{_imageDir}\no_image.jpg";
        Status = "Status";

        StartDownloadCommand = new MvxAsyncCommand(
		() => {
			return Task.Run(
				() => {
                    Status = "Started";
                    _cancellationTokenSource = new CancellationTokenSource();
                    LocalPath = @$"{_imageDir}\no_image.jpg";
                    LocalProgress = 0;
                    var localProgress = new Progress<int>(p => LocalProgress = p);

                    var imageName = @$"{_imageDir}\image{Id + 1}.jpg";

                    if(downloaderService.DownloadImage(Url, imageName, localProgress, _cancellationTokenSource.Token))
                    {
                        Status = "Done";
                        LocalPath = imageName;
                    }
                    else
                    {
                        Status = $"Error: {downloaderService.GetErrorMessage()}";
                        LocalPath = @$"{_imageDir}\no_image.jpg";
                        LocalProgress = 0;
                    }
                });
		});

        StopDownloadCommand = new MvxAsyncCommand(
        () => {
            return Task.Run(
                () => {
                    Status = "Stopped";
                    _cancellationTokenSource?.Cancel();
                });
        });
    }
}
