namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows;  
    using System.Reflection;
    using System.Collections.Specialized;
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

    public class GAScatterPiece : GAMultiPiece
    {

        private Rectangle slice = null;
        private Rectangle selectedSlice = null;


        #region Constructors

        static GAScatterPiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAScatterPiece), new FrameworkPropertyMetadata(typeof(GAScatterPiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GAScatterPiece"/> class.
        /// </summary>
        public GAScatterPiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(GAScatterPiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GAScatterPiece);
#endif
            Loaded += GAScatterPiece_Loaded;
        }

        #endregion Constructors



        #region Methods

        public override Style GetDefaultStyle()
        {
            object o = TryFindResource("GAScatterBulletStyle");
            return (Style)o;
        }

        public override Style GetDefaultSelectedStyle()
        {
            object o = TryFindResource("GAScatterBulletSelectedStyle");
            return (Style)o;
        }

        public override Style GetDefaultLegendStyle()
        {
            object o = TryFindResource("GAScatterPieceLegendStyle");
            return (Style)o;
        }

        
        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GAScatterPiece).DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            slice = this.GetTemplateChild("Slice") as Rectangle;
            selectedSlice = this.GetTemplateChild("SelectionHighlight") as Rectangle;
            RegisterMouseEvents(slice);
        }

        void GAScatterPiece_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGeometry();
        }

        protected override void DrawGeometry(bool withAnimation = true)
        {
            try
            {
                if (this.ClientWidth <= 0.0)
                {
                    return;
                }
                if (this.ClientHeight <= 0.0)
                {
                    return;
                }

                if (slice == null) return;

                double endHeight = ClientHeight;

                if (endHeight <= 0.0)
                {
                    endHeight=0;
                }

                this.Visibility = Visibility.Visible;
                //if ((!IsNegativePiece && Percentage < 0))
                //{
                //    this.Visibility = Visibility.Hidden;
                //}

                //if ((IsNegativePiece && Percentage <= 0))
                //{
                //    this.Visibility = Visibility.Hidden;
                //}

                // I draw the scatter at a zero location and then apply a translation
                // this gets around an issue when drawing the scatter point at the maximum grid location
                // and half of it gets cut off

                //double percentToUse = Percentage < 0 ? 0 : Percentage;
                double axisThickness = ParentChart.xAxisThickness;
                double startHeight = 0;
                double horizontalTranslate = (this.ActualWidth / 2)- getHalfBulletWidth();
                TranslateTransform sliceTransform = new TranslateTransform(horizontalTranslate, 0);

                // if there is an existing translation then use this, dont add a new one
                if (slice.RenderTransform!=null && slice.RenderTransform.GetType()== typeof(TranslateTransform))
                {
                    sliceTransform=(slice.RenderTransform as TranslateTransform);
                    startHeight = sliceTransform.Y;
                }
                else
                {
                    sliceTransform = new TranslateTransform(horizontalTranslate, 0);
                    slice.RenderTransform = sliceTransform;

                }

                if (!IsNegativePiece)
                {
                    //endHeight = (endHeight * percentToUse)+ getHalfBulletHeight();
                    endHeight = (endHeight * Percentage) + getHalfBulletHeight();
                }
                else
                {
                    //endHeight = -1 * ((endHeight * percentToUse)) + getHalfBulletHeight() + axisThickness;
                    endHeight = (endHeight * Percentage) + getHalfBulletHeight() + axisThickness;
                }

                DoubleAnimation moveAnimation = new DoubleAnimation(-endHeight, TimeSpan.FromMilliseconds(500));
                moveAnimation.From = startHeight;
                moveAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
                sliceTransform.BeginAnimation(TranslateTransform.YProperty, moveAnimation);
                selectedSlice.RenderTransform = new TranslateTransform(horizontalTranslate, -endHeight);
                
            }
            catch (Exception ex)
            {
            }
        }

        
        private double getHalfBulletHeight()
        {
             Setter heightSetter= Helpers.getCalculatedSetter(GAChartPieceStyle, "Height", true);
             return (double)heightSetter.Value / 2;
        }

        private double getHalfBulletWidth()
        {

            Setter widthSetter = Helpers.getCalculatedSetter(GAChartPieceStyle, "Width", true);
            return (double)widthSetter.Value / 2;
        }

        #endregion Methods
    }
}