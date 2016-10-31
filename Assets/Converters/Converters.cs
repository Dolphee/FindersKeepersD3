using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace FindersKeepers.Assets.Converters
{
    public class CColorConverter : IValueConverter
    {
       public object Convert(object value, Type targetType,object parameter, CultureInfo culture)
        {
            return value.ToString().ToBrush(false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().TrimStart('#');
        }
    }

    public class CProcent : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double p = (double)value;

            if (p > 1)
                p = 1;

            return (p * 100).ToString(); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueWithoutPercentage = value.ToString().TrimEnd(' ', '%');
            decimal v = decimal.Parse(valueWithoutPercentage) / 100;

            return v > 100 ? 100 : v;
        }
    }

    public class CMarginMenu : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? new Thickness(0) : new Thickness(35, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? new Thickness(35, 0, 0, 0) : new Thickness();
        }
    }


    public class CMarginMenuVisability : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class intToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetName(typeof(LegendaryItemsTypes), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? 457 : 634;
        }
    }


    public class CWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? 457 : 400;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? 400 : 457;
        }
    }

    public class CHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? 400 : 340;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? 300 : 340;
        }
    }

    public class CMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? new Thickness(0) : new Thickness(20, 20, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? new Thickness(20, 20, 0, 0) : new Thickness(0);
        }
    }

    public class CImageChecked : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Extensions.FKImage("checked") : Extensions.FKImage("_checked");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(bool)value) ? 457 : 634;
        }
    }


    public class CReplaceUnderscore : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace('_', ' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Replace(' ', '_');
        }
    }


    public class CVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && !(bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class CMenuBackground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "5085a0".ToBrush(false) : Extensions.ToTransparent(false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "ffffff".ToBrush(false) : Extensions.ToTransparent(false);
        }
    }

    public class CMenuTextForeground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "ffffff".ToBrush(false) : "666666".ToBrush(false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "ffffff".ToBrush(false) : Extensions.ToTransparent();
        }
    }

    public class CMenuBorderBackground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "4a5d67".ToBrush() : Extensions.ToTransparent();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "4a5d67".ToBrush() : Extensions.ToTransparent();
        }
    }

    public class CActive : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? new System.Windows.Duration(new TimeSpan(0)) : new System.Windows.Duration(new TimeSpan(0, 0, 0, 0, 700));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && !(bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class CMenuBorder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Templates.Templating.FKTemplates.IDesignHelper.IMenu sender = value as Templates.Templating.FKTemplates.IDesignHelper.IMenu;

            if (sender.isFirst)
                return new Thickness(1,1,0,1);
            else if (sender.isLast)
                return new Thickness(0,1,1,1);

            return new Thickness(0, 1, 0, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && !(bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class RadiusMenuBorder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Templates.Templating.FKTemplates.IDesignHelper.IMenu sender = value as Templates.Templating.FKTemplates.IDesignHelper.IMenu;

            if (sender.isFirst)
                return new CornerRadius(4, 0, 0, 4);
            else if (sender.isLast)
                return new CornerRadius(0, 4, 4, 0);

            return new CornerRadius(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && !(bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class CMainMenuBG : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "ffffff".ToBrush() : "f0f0f0".ToBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is bool) && (bool)value) ? "ffffff".ToBrush() : Extensions.ToTransparent();
        }
    }
}
