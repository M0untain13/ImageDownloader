using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using CoreProject.ViewModels;

namespace WpfProject.Views;

[MvxViewFor(typeof(MainViewModel))]
public partial class MainWindow : MvxWindow
{
    public MainWindow() => InitializeComponent();
}