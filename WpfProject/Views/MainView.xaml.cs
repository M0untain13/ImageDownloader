using CoreProject.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace WpfProject.Views;

[MvxViewFor(typeof(MainViewModel))]
public partial class MainView : MvxWpfView
{
    public MainView() => InitializeComponent();
}
