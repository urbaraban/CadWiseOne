using CadWiseOne.ViewModels;
using CadWiseTextReplacer;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CadWiseOne.Services
{
    internal class TextItemVMConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TextFileTask file)
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
}
