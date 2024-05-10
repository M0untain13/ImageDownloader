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

	private bool[] _isStarted;
	private int[] _progress;
	public int Progress
	{
		get
		{
			var sum = 0;
			var count = 0;
			for(var i = 0; i < 3; i++)
			{
				if (_isStarted[i])
				{
					count++;
					sum += _progress[i];
				}
			}

            if (count != 0)
			{
                if (sum / count == 100)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        _isStarted[i] = false;
                    }
					sum = 0;
                }
                return sum / count;
            }
			else
			{
                return 0;
            }
        }
    }

	public IMvxAsyncCommand StartAllCommand { get; }

	public MainViewModel()
	{
		_progress = new int[3];
		_isStarted = new bool[3];
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
					var listOfTasks = new List<Task>();
					foreach (var component in _components)
					{
						component.StopDownloadCommand.Execute();
						component.StartDownloadCommand.Execute();
					}
				});
		});
	}

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
		if (sender is not ImageComponentModel component)
			return;

		if (e.PropertyName == "LocalProgress")
		{
			var index = component.Id;
			var progress = component.LocalProgress;

			if (!_isStarted[index] && progress > 0 && progress < 100)
				_isStarted[index] = true;

            _progress[index] = progress;
			RaisePropertyChanged(nameof(Progress));
        }
		else if(e.PropertyName == "CancellationTokenSource")
		{
			if (component.CancellationTokenSource.IsCancellationRequested)
			{
				_isStarted[component.Id] = false;
			}
		}
    }
}
