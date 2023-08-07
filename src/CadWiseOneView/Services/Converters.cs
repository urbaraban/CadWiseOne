using CadWiseOne.ViewModels;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System;
using CadWiseTextFilter;

namespace CadWiseOne.Services
{
    internal class TextItemVMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextFile file)
            {
                return new TextItemVM(file);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden;

        }
    }

    internal class TextTaskVMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextTask task)
            {
                return new TaskVM(task);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden;

        }
    }
}
