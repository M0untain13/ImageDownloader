using CoreProject.ViewModels;
using CoreProject.Services;
using MvvmCross;
using MvvmCross.ViewModels;

namespace CoreProject;

public class Core : MvxApplication
{
    public override void Initialize()
    {
        Mvx.IoCProvider.RegisterType<IDownloaderService, DownloaderService>();
        RegisterAppStart<MainViewModel>();
    }
}
