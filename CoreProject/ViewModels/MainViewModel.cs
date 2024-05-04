using CoreProject.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace CoreProject.ViewModels;

public class MainViewModel : MvxViewModel
{
	private ImageComponentModel[] _components;
	public ImageComponentModel[] Components
	{
		get => _components;
		set => SetProperty(ref _components, value);
	}

    public IMvxAsyncCommand StartAllCommand { get; }

	public MainViewModel()
	{
		_components = new ImageComponentModel[3];
		for(var i = 0; i < 3; i++)
		{
			Mvx.IoCProvider.TryResolve<IDownloaderService>(out var downloaderService);
            _components[i] = new ImageComponentModel(downloaderService);
        }

		StartAllCommand = new MvxAsyncCommand(
		() => {
			return Task.Run(
				() => {
					foreach (var component in _components)
					{
						component.StartDownloadCommand.Execute();
					}
				});
		});
	}
}
