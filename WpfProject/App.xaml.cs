using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace WpfProject;

public partial class App : MvxApplication
{
    public App() => this.RegisterSetupType<Setup>();
}
