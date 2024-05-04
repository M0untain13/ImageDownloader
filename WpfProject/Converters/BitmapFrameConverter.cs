using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows;

namespace WpfProject.Converters;

public sealed class BitmapFrameConverter : IValueConverter
{
    public double DecodePixelWidth { get; set; }
    public double DecodePixelHeight { get; set; }

    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        var path = value as string;

        if (string.IsNullOrEmpty(path)) 
            return DependencyProperty.UnsetValue;

        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
        bitmapImage.DecodePixelWidth = (int)DecodePixelWidth;
        bitmapImage.DecodePixelHeight = (int)DecodePixelHeight;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        bitmapImage.StreamSource.Dispose();

        return bitmapImage;

    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}