namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

#if NETFX_CORE

    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml;

#else

    using System.Windows.Media;
    using System.Windows;

#endif

    /// <summary>
    /// we cannot use the ChartSeries directly because we will join the data to internal Chartseries
    /// </summary>
    public class ChartLegendItemViewModel : DependencyObject
    {
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption",
            typeof(string),
            typeof(ChartLegendItemViewModel),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ItemBrushProperty =
            DependencyProperty.Register("ItemBrush",
            typeof(Brush),
            typeof(ChartLegendItemViewModel),
            new PropertyMetadata(null));

        /// <summary>
        /// The style for the legend piece 
        /// </summary>
        public static readonly DependencyProperty SeriesLegendStyleProperty =
           DependencyProperty.Register("SeriesLegendStyle", typeof(Style), typeof(ChartLegendItemViewModel),
           new PropertyMetadata(null));

        /// <summary>
        /// The stye for each datapoint
        /// </summary>
        public static readonly DependencyProperty DataPointStyleProperty =
            DependencyProperty.Register("DataPointStyle",
            typeof(Style),
            typeof(ChartLegendItemViewModel),
            new PropertyMetadata(null));

        /// <summary>
        /// Show or hide the legend for this series 
        /// </summary>
        public static readonly DependencyProperty SeriesLegendVisibiltyProperty =
           DependencyProperty.Register("SeriesLegendVisibilty",
           typeof(Visibility),
           typeof(ChartLegendItemViewModel),
           new PropertyMetadata(Visibility.Visible));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public Brush ItemBrush
        {
            get { return (Brush)GetValue(ItemBrushProperty); }
            set { SetValue(ItemBrushProperty, value); }
        }

        /// <summary>
        /// The style for the legendItem
        /// </summary>
        public Style SeriesLegendStyle
        {
            get
            {
                return (Style)GetValue(SeriesLegendStyleProperty);
            }
            set
            {
                SetValue(SeriesLegendStyleProperty, value);
            }
        }

        public Style DataPointStyle
        {
            get
            {
                return (Style)GetValue(DataPointStyleProperty);
            }
            set
            {
                SetValue(DataPointStyleProperty, value);
            }
        }

        public Visibility SeriesLegendVisibilty
        {
            get
            {
                return (Visibility)GetValue(SeriesLegendVisibiltyProperty);
            }
            set
            {
                SetValue(SeriesLegendVisibiltyProperty, value);
            }
        }
    }
}
