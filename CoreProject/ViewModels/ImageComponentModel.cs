using CoreProject.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;

namespace CoreProject.ViewModels;

public class ImageComponentModel : MvxViewModel
{
	private string _url;
	public string Url
	{
		get => _url;
		set => SetProperty(ref _url, value);
	}

	public IMvxAsyncCommand StartDownloadCommand { get; }

	public IMvxAsyncCommand StopDownloadCommand { get; }

	public ImageComponentModel(IDownloaderService downloaderService)
	{
		Url = string.Empty;

        StartDownloadCommand = new MvxAsyncCommand(
		() => {
			return Task.Run(
				() => {
					
				});
		});

        StopDownloadCommand = new MvxAsyncCommand(
        () => {
            return Task.Run(
                () => {

                });
        });
    }
}
