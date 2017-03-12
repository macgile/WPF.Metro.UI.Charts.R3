using System;
using System.Collections;
namespace GravityApps.Mandelkow.MetroCharts
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Collections.Specialized;

#if NETFX_CORE
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Shapes;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Data;

#else
    using System.Windows.Media;
    using System.Windows.Controls;
    using System.Windows;
#endif

    public class ChartSeries : ItemsControl
    {
        public enum relativeSeriesColour { Previous, Next, Default };

        public static readonly DependencyProperty DisplayMemberProperty =
            DependencyProperty.Register("DisplayMember",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ValueMemberProperty =
            DependencyProperty.Register("ValueMember",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));
        public static readonly DependencyProperty SeriesTitleProperty =
            DependencyProperty.Register("SeriesTitle",
            typeof(string),
            typeof(ChartSeries),
            new PropertyMetadata(null));

        public static readonly DependencyProperty RelativeSeriesColourProperty =
            DependencyProperty.Register("RelativeSeriesColour",
            typeof(relativeSeriesColour),
            typeof(ChartSeries),
            new PropertyMetadata(relativeSeriesColour.Default));
        
 
        /// <summary>
        /// The Type used to draw the dataPoint (must have GAMultiPiece as a base)
        /// </summary>
        public static readonly DependencyProperty DataPointTypeProperty =
           DependencyProperty.Register("DataPointType",
           typeof(Type),
           typeof(ChartSeries),
           new PropertyMetadata(null));


        /// <summary>
        /// The style for the datapoint piece (ie GAScatterStyle)
        /// </summary>
        public static readonly DependencyProperty DataPointStyleProperty =
           DependencyProperty.Register("DataPointStyle",
           typeof(Style),
           typeof(ChartSeries),
           new PropertyMetadata(null));

        /// <summary>
        /// The style for the selected bullets
        /// empty will default to GAScatterBulletSelectedStyle
        /// </summary>
        public static readonly DependencyProperty DataPointSelectedStyleProperty =
           DependencyProperty.Register("DataPointSelectedStyle",
           typeof(Style),
           typeof(ChartSeries),
           new PropertyMetadata(null));

        /// <summary>
        /// The style for the legend piece 
        /// </summary>
        public static readonly DependencyProperty SeriesLegendStyleProperty =
           DependencyProperty.Register("SeriesLegendStyle",
           typeof(Style),
           typeof(ChartSeries),
           new PropertyMetadata(null));

        /// <summary>
        /// Show or hide the legend for this series 
        /// </summary>
        public static readonly DependencyProperty SeriesLegendVisibiltyProperty =
           DependencyProperty.Register("SeriesLegendVisibilty",
           typeof(Visibility),
           typeof(ChartSeries),
           new PropertyMetadata(Visibility.Visible));

        
        public ChartSeries()
        {   
        }

        public string SeriesTitle
        {
            get
            {
                return (string)GetValue(SeriesTitleProperty);
            }
            set
            {
                SetValue(SeriesTitleProperty, value);
            }
        }

        public string DisplayMember
        {
            get
            {
                return (string)GetValue(DisplayMemberProperty);
            }
            set
            {
                SetValue(DisplayMemberProperty, value);
            }
        }

        public string ValueMember
        {
            get
            {
                return (string)GetValue(ValueMemberProperty);
            }
            set
            {
                SetValue(ValueMemberProperty, value);
            }
        }

        /// <summary>
        /// The style for the bullets
        /// can be left blank for default
        /// </summary>
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

        /// <summary>
        /// The style for the bullets
        /// can be left blank for default
        /// </summary>
        public Type DataPointType
        {
            get
            {
                return (Type)GetValue(DataPointTypeProperty);
            }
            set
            {
                SetValue(DataPointTypeProperty, value);
            }
        }


        /// <summary>
        /// The style for the bullets, lines etc
        /// can be left blank for default
        /// </summary>
        public Style DataPointSelectedStyle
        {
            get
            {
                return (Style)GetValue(DataPointSelectedStyleProperty);
            }
            set
            {
                SetValue(DataPointSelectedStyleProperty, value);
            }
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


        public relativeSeriesColour RelativeSeriesColour
        {
            get
            {
                return (relativeSeriesColour)GetValue(RelativeSeriesColourProperty);
            }
            set
            {
                SetValue(RelativeSeriesColourProperty, value);
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
