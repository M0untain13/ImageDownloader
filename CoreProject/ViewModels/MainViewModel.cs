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

	private int[] _progress;
	public int Progress
	{
		get
		{
			var sum = 0;
			foreach (var progress in _progress)
			{
				sum += progress;
            }
			return sum/3;
		}
    }

	public IMvxAsyncCommand StartAllCommand { get; }

	public MainViewModel()
	{
		_progress = new int[3];
        Components = new ImageComponentModel[3];


        for (var i = 0; i < 3; i++)
		{
			Mvx.IoCProvider.TryResolve<IDownloaderService>(out var downloaderService);
			Components[i] = new ImageComponentModel(downloaderService, i);
            Components[i].PropertyChanged += MainViewModel_PropertyChanged;
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

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
		if (sender is not ImageComponentModel component)
			return;

        if(e.PropertyName == "LocalProgress")
		{
			_progress[component.Id] = component.LocalProgress;
			RaisePropertyChanged(nameof(Progress));
        }
    }
}
