using CoreProject.ViewModels;
using MvvmCross.ViewModels;
using System.Windows.Controls;

namespace WpfProject.Views;

[MvxViewFor(typeof(ImageComponentModel))]
public partial class ImageComponent : UserControl
{
    public ImageComponent()
    {
        InitializeComponent();
    }
}
