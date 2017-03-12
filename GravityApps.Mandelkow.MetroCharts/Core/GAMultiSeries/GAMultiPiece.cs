namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Collections.Specialized;
    using System.Windows.Input;
    using System.Windows.Data;
    using System.Windows;

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
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    
#endif

    public class GAMultiPiece : PieceBase
    {
        #region Fields

        private Grid slice = null;
        private GAMultiPiece piece = null;

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(GAMultiPiece),
            new PropertyMetadata(0.0, new PropertyChangedCallback(OnPercentageChanged)));

        public static readonly DependencyProperty IsNegativePieceProperty =
            DependencyProperty.Register("IsNegativePiece", typeof(bool), typeof(GAMultiPiece),
            new PropertyMetadata(false, new PropertyChangedCallback(OnPercentageChanged)));

        
        public static readonly DependencyProperty ColumnHeightProperty =
            DependencyProperty.Register("ColumnHeight", typeof(double), typeof(GAMultiPiece),
            new PropertyMetadata(0.0));

        public static readonly DependencyProperty GAChartPieceStyleProperty =
         DependencyProperty.Register("GAChartPieceStyle", typeof(Style), typeof(GAMultiPiece),
         new PropertyMetadata(null));

        public static readonly DependencyProperty GASelectedChartPieceStyleProperty =
        DependencyProperty.Register("GASelectedChartPieceStyle", typeof(Style), typeof(GAMultiPiece),
        new PropertyMetadata(null));

        public static readonly DependencyProperty DataPointTypeProperty =
        DependencyProperty.Register("DataPointType", typeof(Type), typeof(GAMultiPiece),
        new PropertyMetadata(null));

        public static readonly DependencyProperty DataPointGroupIndexProperty =
        DependencyProperty.Register("DataPointGroupIndex", typeof(int), typeof(GAMultiPiece),
        new PropertyMetadata(-1));

        public static readonly DependencyProperty DataPointIndexProperty =
DependencyProperty.Register("DataPointIndex", typeof(int), typeof(GAMultiPiece),
new PropertyMetadata(-1));

        public static readonly DependencyProperty SeriesLegendStyleProperty =
DependencyProperty.Register("SeriesLegendStyle", typeof(Style), typeof(GAMultiPiece),
new PropertyMetadata(null));

        
        #endregion Fields

        #region Constructors

        static GAMultiPiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAMultiPiece), new FrameworkPropertyMetadata(typeof(GAMultiPiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GAMultiPiece"/> class.
        /// </summary>
        public GAMultiPiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(GAMultiPiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GAMultiPiece);
#endif
            Loaded += GAMultiPiece_Loaded;
        }

        #endregion Constructors

        #region Properties

        public Style GAChartPieceStyle
        {
            get
            {
                return (Style)GetValue(GAChartPieceStyleProperty);
            }
            set
            {
                SetValue(GAChartPieceStyleProperty, value);
            }
        }
        
        public Style GASelectedChartPieceStyle
        {
            get
            {
                return (Style)GetValue(GASelectedChartPieceStyleProperty);
            }
            set
            {
                SetValue(GASelectedChartPieceStyleProperty, value);
            }
        }

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

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }
        
        /// <summary>
        /// is thew area the piece is drawn in a negative or positive one
        /// </summary>
        public bool IsNegativePiece
        {
            get { return (bool)GetValue(IsNegativePieceProperty); }
            set { SetValue(IsNegativePieceProperty, value); }
        }

        public double ColumnHeight
        {
            get { return (double)GetValue(ColumnHeightProperty); }
            set { SetValue(ColumnHeightProperty, value); }
        }

        public Type DataPointType
        {
            get { return (Type)GetValue(DataPointTypeProperty); }
            set { SetValue(DataPointTypeProperty, value); }
        }

        public int DataPointGroupIndex
        {
            get { return (int)GetValue(DataPointGroupIndexProperty); }
            set { SetValue(DataPointGroupIndexProperty, value); }
        }

        public int DataPointIndex
        {
            get { return (int)GetValue(DataPointIndexProperty); }
            set { SetValue(DataPointIndexProperty, value); }
        }
        
 
        #endregion Properties

        #region Methods

        /// <summary>
        /// return the default style for the chartPiece
        /// Ovveride in the sub-classes.
        /// </summary>
        /// <returns></returns>
        public virtual Style GetDefaultStyle()
        {
           // object o = TryFindResource("GAScatterBulletSelectedStyle");
            return null;
        }

        /// <summary>
        /// return the default style for the selected chartPiece
        /// Ovveride in the sub-classes.
        /// </summary>
        /// <returns></returns>
        public virtual Style GetDefaultSelectedStyle()
        {
            // object o = TryFindResource("GAScatterBulletSelectedStyle");
            return null;
        }

        /// <summary>
        /// return the default style for the selected chartPiece
        /// Ovveride in the sub-classes.
        /// </summary>
        /// <returns></returns>
        public virtual Style GetDefaultLegendStyle()
        {
            // object o = TryFindResource("GAScatterBulletSelectedStyle");
            return null;
        }

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GAMultiPiece).DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            slice = this.GetTemplateChild("MultiSlice") as Grid;
           // RegisterMouseEvents(slice); register the mouse events in each piece
        }

        void GAMultiPiece_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGeometry();
        }

        protected override void DrawGeometry(bool withAnimation = true)
        {
            try
            {
                if (!shouldRender())
                {
                    return;
                }

                if (slice.Children.Count==0)
                {
                    slice.Children.Add((UIElement)Activator.CreateInstance(DataPointType));
                }

                piece = (GAMultiPiece)slice.Children[0]; // the piece (GAColumn, GAScatter etc)

                generateBinding(BindingMode.TwoWay, "IsClickedByUser", piece, GAMultiPiece.IsClickedByUserProperty);
                generateBinding(BindingMode.TwoWay, "IsSelected", piece, GAMultiPiece.IsSelectedProperty);
                generateBinding(BindingMode.OneWay, "Percentage", piece, GAMultiPiece.PercentageProperty);
                generateBinding(BindingMode.OneWay, "IsNegativePiece", piece, GAMultiPiece.IsNegativePieceProperty);
                generateBinding(BindingMode.OneWay, "ClientWidth", piece, GAMultiPiece.ClientWidthProperty);
                generateBinding(BindingMode.OneWay, "ClientHeight", piece, GAMultiPiece.ClientHeightProperty);
                generateBinding(BindingMode.OneWay, "GAChartPieceStyle", piece, GAMultiPiece.GAChartPieceStyleProperty);
                generateBinding(BindingMode.OneWay, "GASelectedChartPieceStyle", piece, GAMultiPiece.GASelectedChartPieceStyleProperty);
                generateBinding(BindingMode.OneWay, "Background", piece, GAMultiPiece.BackgroundProperty);
                generateBinding(BindingMode.OneWay, "SelectedBrush", piece, GAMultiPiece.SelectedBrushProperty);
                generateBinding(BindingMode.OneWay, "ParentChart", piece, GAMultiPiece.ParentChartProperty);
                generateBinding(BindingMode.OneWay, "DataPointGroupIndex", piece, GAMultiPiece.DataPointGroupIndexProperty);
                generateBinding(BindingMode.OneWay, "DataPointIndex", piece, GAMultiPiece.DataPointIndexProperty);
            }
            catch (Exception ex)
            {
            }
        }

        private void generateBinding(BindingMode mode,String path,DependencyObject target, DependencyProperty property)
        {
            var thisBinding = new Binding();
            thisBinding.Source = this;
            thisBinding.Mode = mode;
            thisBinding.Path = new PropertyPath(path);
            BindingOperations.SetBinding(target, property, thisBinding);
        }

        /// <summary>
        /// should the rendering of this continue?
        /// </summary>
        /// <returns></returns>
        protected virtual bool shouldRender()
        {

            if (this.ClientWidth <= 0.0 || this.ClientHeight <= 0.0 || slice == null)
            {
                return false;
            }
            return true; 
        }

        #endregion Methods
    }
}