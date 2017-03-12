using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

#else

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

#endif

namespace GravityApps.Mandelkow.MetroCharts
{
    public class GASingleColumnsGrid : Panel
    {
        public GASingleColumnsGrid()
        {
            // this.Background = new SolidColorBrush(Colors.Green);

        }

        protected override Size MeasureOverride(Size availableSize)
        {
            try
            {
                /*
                if (double.IsInfinity(availableSize.Width))
                {
                    availableSize.Width = 1000;
                }
                if (double.IsInfinity(availableSize.Height))
                {
                    availableSize.Height = 1000;
                }
                */
                //gleichmäßige Verteilung, deshalb suchen wir die breiteste Column und multiplizieren mit Anzahl der Spalten
                double maxColumnWidth = 0.0;
                double minColumnHeight = 0.0;

                foreach (UIElement child in Children)
                {
                    if (Children.Count > 1)
                    {
                    }
                    child.Measure(availableSize);
                    if (maxColumnWidth < child.DesiredSize.Width)
                    {
                        maxColumnWidth = child.DesiredSize.Width;
                    }
                    if (minColumnHeight < child.DesiredSize.Height)
                    {
                        minColumnHeight = child.DesiredSize.Height;
                    }
                }
                availableSize.Width = maxColumnWidth * Children.Count;
                availableSize.Height = minColumnHeight;


                /*

                Size cellSize = GetCellSize(internalAvailableSize);

                //is there any element which would exceed the cell width
                if (OneElementExceedsCellWidth(cellSize.Width))
                {
                    //we switch to 2 rows, we need the order space for 2 rows
                    double heightOfOneRow = GetHighestElement();
                    return new Size(internalAvailableSize.Width, heightOfOneRow * 2);
                }
                 * */

                return availableSize;
            }
            catch (Exception ex)
            {
                return new Size(0, 0);
            }
        }

        private double GetHighestElement()
        {
            double highestElementHeight = 0.0;
            foreach (UIElement child in Children)
            {
                if (child.DesiredSize.Height > highestElementHeight)
                {
                    highestElementHeight = child.DesiredSize.Height;
                }
            }
            return highestElementHeight;
        }

        private Size GetCellSize(Size availableSize)
        {
            //return new Size(availableSize.Width / Children.Count, availableSize.Height);
            // size to fit only 1 panel as they are put one on top of the other
            return new Size(availableSize.Width, availableSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //calculate the space for each column
            Size cellSize = GetCellSize(finalSize);
            double cellWidth = cellSize.Width;
            double cellHeight = cellSize.Height;

            int col = 0;
            foreach (UIElement child in Children)
            {
                double middlePointX = cellSize.Width * col + cellSize.Width / 2.0;
                child.Arrange(new Rect(new Point(middlePointX - cellWidth / 2.0, 0.0), new Size(cellWidth, cellHeight)));
                // col++; only want 1 column now
            }

            return finalSize;
        }
    }
}
