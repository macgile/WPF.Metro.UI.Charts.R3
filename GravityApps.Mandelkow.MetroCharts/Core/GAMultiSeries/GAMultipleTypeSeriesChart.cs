namespace GravityApps.Mandelkow.MetroCharts
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

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
#else
    using System.Windows.Media;
    using System.Windows.Controls;
#endif

    /// <summary>
    /// Represents an Instance of the bar-chart
    /// </summary>
    public class GAMultipleTypeSeriesChart : ChartBase
    {
        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="GAMultipleTypeSeriesChart"/> class.
        /// </summary>
        static GAMultipleTypeSeriesChart()
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAMultipleTypeSeriesChart), new FrameworkPropertyMetadata(typeof(GAMultipleTypeSeriesChart)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GAMultipleTypeSeriesChart"/> class.
        /// </summary>
        public GAMultipleTypeSeriesChart()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(GAMultipleTypeSeriesChart);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GAMultipleTypeSeriesChart);
#endif
        }

        #endregion Constructors

        protected override double GridLinesMaxValue
        {
            get
            {
                
                return MaxDataPointValue;
            }
        }

        protected override double GridLinesMinValue
        {
            get
            {
                return MinDataPointValue;
            }
        }

        protected override void OnMaxDataPointValueChanged(double p)
        {
            UpdateGridLines();
            UpdateSeries();
        }

        public override bool IsUseNextBiggestMaxValue
        {
            get
            {
                return true;
            }
        }
    }
}
