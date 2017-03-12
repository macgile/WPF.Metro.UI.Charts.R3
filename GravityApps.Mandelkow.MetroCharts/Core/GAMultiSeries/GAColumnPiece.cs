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
    using System.Windows.Data;

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

    public class GAColumnPiece : GAMultiPiece
    {


        private Border slice = null;
        bool isNegativePiece = false;
        AutoSizeTextBlock bottomText = null;
        AutoSizeTextBlock topText = null;

        public static readonly DependencyProperty TextExceedsHeightProperty =
           DependencyProperty.Register("TextExceedsHeight", typeof(bool), typeof(GAColumnPiece),
           new PropertyMetadata(false, new PropertyChangedCallback(OnPercentageChanged)));

        public bool TextExceedsHeight
        {
            get { return (bool)GetValue(TextExceedsHeightProperty); }
            set { SetValue(TextExceedsHeightProperty, value); }
        }

        #region Constructors

        static GAColumnPiece()        
        {
#if NETFX_CORE
                        
#elif SILVERLIGHT
    
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GAColumnPiece), new FrameworkPropertyMetadata(typeof(GAColumnPiece)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GAColumnPiece"/> class.
        /// </summary>
        public GAColumnPiece()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(GAColumnPiece);
#endif
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GAColumnPiece);
#endif
            Loaded += ColumnPiece_Loaded;
        }

        #endregion Constructors



        #region Methods

        public override Style GetDefaultStyle()
        {
            object o = TryFindResource("GAColumnPieceStyle");
            return (Style)o;
        }

        public override Style GetDefaultSelectedStyle()
        {
            object o = TryFindResource("GAColumnPieceSelectedStyle");
            return (Style)o;
        }

        public override Style GetDefaultLegendStyle()
        {
            object o = TryFindResource("GAColumnPieceLegendStyle");
            return (Style)o;
        }

        private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as GAColumnPiece).DrawGeometry();
        }

        protected override void InternalOnApplyTemplate()
        {
            slice = this.GetTemplateChild("Slice") as Border;
            RegisterMouseEvents(slice);
            setup();
        }


        private void setup()
        {
            bottomText = this.GetTemplateChild("BottomNumber") as AutoSizeTextBlock;
            topText = this.GetTemplateChild("TopNumber") as AutoSizeTextBlock;
            
            Grid mainGrid = (Grid)this.Parent;
            GAMultiPiece piece = (GAMultiPiece)mainGrid.TemplatedParent;
            isNegativePiece = piece.Name == "NegativeMultiPiece";
            AutoSizeTextBlock MainTextBlock = this.GetTemplateChild("MainTextBlock") as AutoSizeTextBlock; ;

            // Bind mainTextBox IsHeightExceedsSpaceProperty to this TextExceedsHeight
            var mainTextBlockBinding = new Binding();
            mainTextBlockBinding.Source = this;
            mainTextBlockBinding.Mode = BindingMode.OneWayToSource;
            mainTextBlockBinding.Path = new PropertyPath("TextExceedsHeight");
            BindingOperations.SetBinding(MainTextBlock, AutoSizeTextBlock.IsHeightExceedsSpaceProperty, mainTextBlockBinding);

        }

        void ColumnPiece_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGeometry();
        }


        protected override void DrawGeometry(bool withAnimation = true)
        {
            if (slice == null) return;
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

                if (this.Percentage < 0)
                {
                    this.Visibility = Visibility.Collapsed;
                    return;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                }

                if (bottomText!=null)
                {
                    bottomText.Visibility = getBottomTextVisibilty(Percentage, isNegativePiece, TextExceedsHeight);
                }

                if (topText != null)
                {
                    topText.Visibility = getTopTextVisibilty(Percentage, isNegativePiece, TextExceedsHeight);
                }
               

                double endHeight = ClientHeight;
                double percentToUse = Percentage;

                double startHeight = 0;
                if (slice.Height > 0)
                {
                    startHeight = slice.Height;
                }

                if (percentToUse < 0) percentToUse = 0;
                endHeight = endHeight * percentToUse;

               
                DoubleAnimation scaleAnimation = new DoubleAnimation();
                scaleAnimation.From = startHeight;
                scaleAnimation.To = endHeight;

                scaleAnimation.Duration = TimeSpan.FromMilliseconds(withAnimation ? 500: 0);
                scaleAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
                Storyboard storyScaleX = new Storyboard();
                storyScaleX.Children.Add(scaleAnimation);

                Storyboard.SetTarget(storyScaleX, slice);

#if NETFX_CORE
                scaleAnimation.EnableDependentAnimation = true;
                Storyboard.SetTargetProperty(storyScaleX, "Height");
#else
                Storyboard.SetTargetProperty(storyScaleX, new PropertyPath("Height"));
#endif
                storyScaleX.Begin();
   
            }
            catch (Exception ex)
            {
            }
        }

        private Visibility getBottomTextVisibilty(double Percentage, bool isNegativePiece, bool heightExceedsSpace)
        {       
            if (isNegativePiece && heightExceedsSpace && Percentage!=0) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        private Visibility getTopTextVisibilty(double Percentage, bool isNegativePiece, bool heightExceedsSpace)
        {
            if (!isNegativePiece && Percentage == 0) return Visibility.Visible;
            if (!isNegativePiece && heightExceedsSpace) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        #endregion Methods
    }
}