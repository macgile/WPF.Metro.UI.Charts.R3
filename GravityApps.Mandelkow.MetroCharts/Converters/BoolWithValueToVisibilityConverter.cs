namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

#if NETFX_CORE
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml;
#else
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Data;
#endif

    /// <summary>
    /// Used to show and hide the data tags on the column chart
    /// information[0] should be the value
    //  information[1] should be isHEightExceeded
    //  information[2] should be "positive" or "negative" depending on which series it is ??
    /// </summary>
    public sealed class BoolWithValueToVisibilityConverter : IMultiValueConverter
    {

#if NETFX_CORE

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return InternalConvertBack(value, targetType, parameter);
        }

#else
        public object Convert(object [] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return InternalConvert(value, targetType, parameter);
        }

        public object[] ConvertBack(object value, Type [] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return InternalConvertBack(value, targetType, parameter);
        }

#endif

        private object InternalConvert(object [] information, Type targetType, object dataPointValue)
        {
            try
            {
                bool captionOK = true;
                if (information.Count() >= 4 && string.IsNullOrEmpty((string)information[3])) return Visibility.Collapsed; 

            double value = (double)information[0];
            bool isHeightExceeded = (bool)information[1];
            bool topNumberLocation = (string)information[2] == "Top";

            // if percent<0 then the piece is already off so this isnt a problem
            //if value<0 then its a negative piece

            bool isNegative = value < 0;


            //for negative piece, never want to show top text
            // for positive piece, never want to show bottom text
            if (topNumberLocation && isNegative) return Visibility.Collapsed;
            if (!topNumberLocation && !isNegative) return Visibility.Collapsed;


            

                var visible = true;

                if (topNumberLocation && !isHeightExceeded && !isNegative) visible = false;
                if (!topNumberLocation && !isHeightExceeded && isNegative) visible = false;
                if (!topNumberLocation && value == 0) visible = false;
               

                if (visible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
                
            }
            catch (Exception ex)
            {
            }
            return Visibility.Visible;

        }

        public object[] InternalConvertBack(object value, Type[] targetType, object parameter)
        {
            //var back = ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
            //if (parameter != null)
            //{
            //    if ((bool)parameter)
            //    {
            //        back = !back;
            //    }
            //}
            throw new NotSupportedException();
        }
    }
}
