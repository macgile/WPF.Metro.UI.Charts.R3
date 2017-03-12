namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Data;
    using System.Reflection;
    using System.Collections.Specialized;
    using System.Collections.ObjectModel;
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
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    
#endif

    public class GALinePiece : GAMultiPiece
    {

        private Path slice = null;
        private Path selectedSlice = null;
        private Grid parentGrid = null;
        private int _animationTime = 500;

        public static readonly DependencyProperty DataPointsProperty =
          DependencyProperty.Register("DataPoints", typeof(ObservableCollection<DataPoint>), typeof(GALinePiece),
          new PropertyMetadata(new ObservableCollection<DataPoint>(), new PropertyChangedCallback(OnPercentageChanged)));

        public ObservableCollection<DataPoint> DataPoints
        {
            get { return (ObservableCollection<DataPoint>)GetValue(DataPointsProperty); }
            set { SetValue(DataPointsProperty, value); }
        }

        #region Constructors

        static GALinePiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GALinePiece), new FrameworkPropertyMetadata(typeof(GALinePiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GALinePiece"/> class.
        /// </summary>
        public GALinePiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(GALinePiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GALinePiece);
#endif
            Loaded += GALinePiece_Loaded;
        }

        #endregion Constructors



        #region Methods

        public override Style GetDefaultStyle()
        {
            object o = TryFindResource("GALineStyle");
            return (Style)o;
        }
        // this doesnt support selected lines yet
        public override Style GetDefaultSelectedStyle()
        {
            object o = TryFindResource("GALineStyle");
            return (Style)o;
        }

        public override Style GetDefaultLegendStyle()
        {
            object o = TryFindResource("GALinePieceLegendStyle");
            return (Style)o;
        }

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GALinePiece).DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            slice = this.GetTemplateChild("Slice") as Path;
           
            parentGrid = this.Parent as Grid;
           
            loadDataPoints();
            //selectedSlice = this.GetTemplateChild("SelectionHighlight") as Path;
          //  RegisterMouseEvents(slice);
        }

        private void loadDataPoints()
        {
            if (ParentChart!=null)
            {
                DataPoints = ParentChart.AllDataPoints;
            }
        }

       
        void DataPointGroup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                
                moveDataPoint((DataPoint)sender);
            }
        }

        private void moveDataPoint(DataPoint newPoint)
        {
            if (!shouldBeVisibile())
                {
                    return;
                }
            try
            {

            double width = parentGrid.RenderSize.Width;
            double positiveHeight = ParentChart.positiveChartAreaHeight;
            double halfAxisThickness = ParentChart.xAxisThickness / 2;

            GeometryGroup linesGeom = (GeometryGroup)slice.Data;

            double offset = newPoint.PercentageFromMaxDataPointValue < 0 ? -halfAxisThickness : halfAxisThickness; // allow for the x axis thickness

            double CenterX = (newPoint.DataPointIndex * width) + (width / 2);
            double CenterY = 0;
            CenterY = (-1 * newPoint.PercentageFromMaxDataPointValue * positiveHeight) + offset;

            newPoint.PropertyChanged -= DataPointGroup_PropertyChanged;
            moveLinePoints(newPoint, CenterX, CenterY, linesGeom);
            newPoint.PropertyChanged += DataPointGroup_PropertyChanged;
                
            }
            catch (Exception ex)
            {

            }


        }

        /// <summary>
        /// Move the existing line points
        /// This may include moving multiple or single lines
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="CenterX"></param>
        /// <param name="CenterY"></param>
        /// <param name="linesGeom"></param>
        private void moveLinePoints(DataPoint newPoint, double CenterX, double CenterY, GeometryGroup linesGeom)
        {

            if (newPoint.DataPointIndex == 0)
            {
                animateLine(CenterX, CenterY, linesGeom, LineGeometry.StartPointProperty, 0);
                return;
            }
            if (newPoint.DataPointIndex == linesGeom.Children.Count)
            {
                animateLine(CenterX, CenterY, linesGeom, LineGeometry.EndPointProperty, linesGeom.Children.Count - 1);
            }
            else
            {
                animateLine(CenterX, CenterY, linesGeom, LineGeometry.EndPointProperty, newPoint.DataPointIndex - 1);
                animateLine(CenterX, CenterY, linesGeom, LineGeometry.StartPointProperty, newPoint.DataPointIndex);
            }
        }

        /// <summary>
        /// move and animate a single line in the group of datapoints
        /// </summary>
        /// <param name="CenterX"></param>
        /// <param name="CenterY"></param>
        /// <param name="linesGeom"></param>
        /// <param name="dp"></param>
        /// <param name="location"></param>
        private void animateLine(double CenterX, double CenterY, GeometryGroup linesGeom, DependencyProperty dp, int location)
        {
            LineGeometry thisLine = (LineGeometry)linesGeom.Children[location];
            PointAnimation movePointAnimation = new PointAnimation(new Point(CenterX, CenterY), TimeSpan.FromMilliseconds(_animationTime));
            movePointAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            thisLine.BeginAnimation(dp, movePointAnimation);
        }



        void GALinePiece_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGeometry();
        }

        protected override void DrawGeometry(bool withAnimation = true)
        {
            
            try
            {
                if (!shouldBeVisibile())
                {
                    return;
                }
                

                double width = parentGrid.RenderSize.Width;
                int pointCounter = 0;
                Point lineStartPoint = new Point(0, 0);
                Point lineEndPoint = new Point(0, 0);

                GeometryCollection geomCollection = new GeometryCollection();
                GeometryGroup group = new GeometryGroup();
                group.Children = geomCollection;
                slice.Data = group;


                double positiveHeight = ParentChart.positiveChartAreaHeight;
                double halfAxisThickness = ParentChart.xAxisThickness/2;

                foreach (DataPoint point in DataPoints)
                {
                    if (point.DataPointGroupIndex < this.DataPointGroupIndex)
                    {
                        continue;
                    }
                    if (point.DataPointGroupIndex > this.DataPointGroupIndex)
                    {
                        break;
                    }
                    point.PropertyChanged -= DataPointGroup_PropertyChanged;
                    double offset = point.PercentageFromMaxDataPointValue < 0 ? -halfAxisThickness : halfAxisThickness; // allow for the x axis thickness
                    
                    double CenterX = (pointCounter * width) + (width / 2);
                    double CenterY = (-1 * point.PercentageFromMaxDataPointValue * positiveHeight) + offset;

                    if (pointCounter == 0) // 1st point on line graph is a line start only
                    {
                        lineStartPoint = new Point(CenterX, CenterY);
                    }
                    else
                    {
                        lineEndPoint = new Point(CenterX, CenterY);
                        LineGeometry l = new LineGeometry();
                        l.StartPoint = lineStartPoint;
                        l.EndPoint = lineEndPoint;
                        group.Children.Add(l);

                        lineStartPoint.X = CenterX;
                        lineStartPoint.Y = CenterY;
                    }

                    point.OldValue = point.Value;
                    point.PropertyChanged += DataPointGroup_PropertyChanged;
                    pointCounter++;
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private bool shouldBeVisibile()
        {

            if (this.ClientWidth <= 0.0)
            {
                return false;
            }
            if (this.ClientHeight <= 0.0)
            {
                return false;
            }


            if (slice == null || parentGrid == null) return false;


            if (this.DataPointIndex == -1)
            {
                return false;
            }

            if (this.DataPointIndex != 0 || this.IsNegativePiece)
            {
                this.Visibility = Visibility.Collapsed;
                return false;
            }
            return true;
        }

        #endregion Methods
    }
}