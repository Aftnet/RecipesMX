using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Recipes_MX.Tools
{
    public class SearchBoxQuerySubmittedEventArgsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (SearchBoxQuerySubmittedEventArgs)value;
            var element = (FrameworkElement)parameter;

            var Output = args.QueryText;
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
