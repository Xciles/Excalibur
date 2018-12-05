using System;
using System.Globalization;
using System.Threading.Tasks;
using Excalibur.Avalon.Utils;
using MvvmCross.Converters;

namespace Excalibur.Cross.Converters
{
    /// <inheritdoc />
    public abstract class AsyncMvxValueConverter : IMvxValueConverter
    {
        /// <inheritdoc />
        object IMvxValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return NotifyTask.Create(Convert(value, targetType, parameter, culture));
        }

        /// <inheritdoc />
        object IMvxValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return NotifyTask.Create(ConvertBack(value, targetType, parameter, culture));
        }

        public abstract Task<object> Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract Task<object> ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
