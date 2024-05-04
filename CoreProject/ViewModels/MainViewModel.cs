using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace CoreProject.ViewModels;

internal class MainViewModel : MvxViewModel
{
	private readonly ImageComponentModel[] _components;

	public IMvxAsyncCommand StartAllCommand { get; }

	public MainViewModel()
	{
		_components = new ImageComponentModel[3];

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
