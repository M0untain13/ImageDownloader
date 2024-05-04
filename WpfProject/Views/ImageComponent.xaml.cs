using CoreProject.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace WpfProject.Views;

[MvxViewFor(typeof(ImageComponentModel))]
public partial class ImageComponent : MvxWpfView
{
    public ImageComponent()
    {
        InitializeComponent();
    }
}
