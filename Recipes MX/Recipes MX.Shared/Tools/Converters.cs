using Recipes_MX.Logic.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Recipes_MX.Tools
{
    public class ItemClickEventArgsToRecipeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (ItemClickEventArgs)value;
            var element = (FrameworkElement)parameter;

            var Output = (Recipe)args.ClickedItem;
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ItemClickEventArgsToFeaturedEntryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (ItemClickEventArgs)value;
            var element = (FrameworkElement)parameter;

            var Output = (FeaturedEntry)args.ClickedItem;
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (bool)value;
            var element = (FrameworkElement)parameter;

            var Output = args ? Visibility.Visible : Visibility.Collapsed;
            return Output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}
