using CoreProject.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Diagnostics;
using System.Threading;

namespace CoreProject.ViewModels;

public class ImageComponentModel : MvxViewModel
{
    public int Id { get; private set; }

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

    private CancellationTokenSource _cancellationTokenSource;

    public IMvxAsyncCommand StartDownloadCommand { get; }

	public IMvxAsyncCommand StopDownloadCommand { get; }

    public ImageComponentModel(IDownloaderService downloaderService, int id = -1)
	{
        Id = id;
        Url = string.Empty;
        LocalPath = "no_image.jpg";
        _cancellationTokenSource = new CancellationTokenSource();


        StartDownloadCommand = new MvxAsyncCommand(
		() => {
			return Task.Run(
				() => {

                    var localProgress = new Progress<int>(p => LocalProgress = p);

                    try
                    {
                        downloaderService.DownloadImage(Url, localProgress, _cancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        // TODO: Надо обрабатывать нажатие кнопки СТОП
                    }
                });
		});

        StopDownloadCommand = new MvxAsyncCommand(
        () => {
            return Task.Run(
                () => {
                    _cancellationTokenSource.Cancel();
                });
        });
    }
}
