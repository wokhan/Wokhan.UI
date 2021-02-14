using System;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using System.Reflection;
#if __WPF__
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#else
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
#endif

namespace Wokhan.UI.BindingConverters
{
    public sealed class ImageSourceConverter : IValueConverter
    {
        private readonly Regex srcReg = new Regex("/(?<assembly>.*?);(?<path>.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapFrame bitmapFrame = null;
            var res = srcReg.Match((string)value);
            if (!res.Success)
            {
                throw new ArgumentOutOfRangeException("Input format must follow the /{assembly};{path_to_resource} scheme");
            }

            if (!targetType.IsAssignableFrom(typeof(ImageSource)))
            {
                throw new ArgumentOutOfRangeException("Target type must expect an ImageSource object");
            }

            var stream = Assembly.Load(res.Groups["assembly"].Value).GetManifestResourceStream(res.Groups["path"].Value);
#if __WPF__
            var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.Default);
#endif

#if __UAP__
            var decoderTask = BitmapDecoder.CreateAsync(stream.AsRandomAccessStream()).AsTask();
            decoderTask.Wait();
            var decoder = decoderTask.Result;
#endif

#if __WPF__
            bitmapFrame = decoder.Frames[0];
#endif

#if __UAP__
            var bitmapTask = decoderTask.Result.GetFrameAsync(0).AsTask();
            bitmapTask.Wait();
            bitmapFrame = bitmapTask.Result;
#endif

            return bitmapFrame;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value, targetType, parameter, String.Empty);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ConvertBack(value, targetType, parameter, String.Empty);

    }
}
