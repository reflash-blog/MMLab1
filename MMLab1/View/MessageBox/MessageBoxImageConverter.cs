using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MMLab1.View.MessageBox
{
    [ValueConversion(typeof(MessageBoxImage), typeof(ImageSource))]
    public class MessageBoxImageConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBoxImage image = (MessageBoxImage)value;
            switch (image)
            {
                case MessageBoxImage.Information: /* Asterisk has the same value. */
                    return new BitmapImage(new Uri("pack://application:,,,/View/MessageBox/Resources/Information.png"));
                case MessageBoxImage.Error: /* Hand and Stop have the same value. */
                    return new BitmapImage(new Uri("pack://application:,,,/View/MessageBox/Resources/Error.png"));
                case MessageBoxImage.Warning: /* Exclamation has the same value. */
                    return new BitmapImage(new Uri("pack://application:,,,/View/MessageBox/Resources/Warning.png"));
                case MessageBoxImage.Question:
                    return new BitmapImage(new Uri("pack://application:,,,/View/MessageBox/Resources/Question.png"));
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
