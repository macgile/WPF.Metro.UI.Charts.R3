namespace GravityApps.Mandelkow.MetroCharts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Collections.ObjectModel;

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
    using System.Windows;
#endif

    public class DataPoint : DependencyObject, INotifyPropertyChanged
    {
        //GA added these - MaxDatapoint and MaxGridLIne can be differnt
        public static readonly DependencyProperty MaxPositiveGridLineValueProperty =
   DependencyProperty.Register("MaxPositiveGridLineValue",
   typeof(double),
   typeof(DataPoint),
   new PropertyMetadata(0.0, new PropertyChangedCallback(MaxDataPointValueChanged)));

        public static readonly DependencyProperty MaxNegativeGridLineValueProperty =
   DependencyProperty.Register("MaxNegativeGridLineValue",
   typeof(double),
   typeof(DataPoint),
   new PropertyMetadata(0.0, new PropertyChangedCallback(MaxDataPointValueChanged)));

        // A style for the given datapoint - can allow for more variation that the item brush below
        public static readonly DependencyProperty GADataPointStyleProperty =
   DependencyProperty.Register("GADataPointStyle",
   typeof(Style),
   typeof(DataPoint),
   null);

        public static readonly DependencyProperty GASelectedDataPointStyleProperty =
DependencyProperty.Register("GASelectedDataPointStyle",
typeof(Style),
typeof(DataPoint),
null);

        public static readonly DependencyProperty GADataPointTypeProperty =
DependencyProperty.Register("GADataPointType",
typeof(Type),
typeof(DataPoint),
null);


        public static readonly DependencyProperty MaxDataPointValueProperty =
           DependencyProperty.Register("MaxDataPointValue",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, new PropertyChangedCallback(MaxDataPointValueChanged)));

        public static readonly DependencyProperty MinDataPointValueProperty =
   DependencyProperty.Register("MinDataPointValue",
   typeof(double),
   typeof(DataPoint),
   new PropertyMetadata(0.0, new PropertyChangedCallback(MaxDataPointValueChanged)));

        public static readonly DependencyProperty MaxDataPointGroupSumProperty =
           DependencyProperty.Register("MaxDataPointGroupSum",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, new PropertyChangedCallback(MaxDataPointGroupSumChanged)));

        public static readonly DependencyProperty SumOfDataPointGroupProperty =
           DependencyProperty.Register("SumOfDataPointGroup",
           typeof(double),
           typeof(DataPoint),
           new PropertyMetadata(0.0, new PropertyChangedCallback(SumOfDataPointGroupChanged)));

        public static readonly DependencyProperty StartValueProperty =
          DependencyProperty.Register("StartValue",
          typeof(double),
          typeof(DataPoint),
          new PropertyMetadata(0.0));

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
          typeof(bool),
          typeof(DataPoint),
          new PropertyMetadata(false));

        public static readonly DependencyProperty SelectedBrushProperty =
          DependencyProperty.Register("SelectedBrush",
          typeof(Brush),
          typeof(DataPoint),
          new PropertyMetadata(null));

        public static readonly DependencyProperty ItemBrushProperty =
         DependencyProperty.Register("ItemBrush",
         typeof(Brush),
         typeof(DataPoint),
         new PropertyMetadata(null));

        public static readonly DependencyProperty IsClickedByUserProperty =
          DependencyProperty.Register("IsClickedByUser",
          typeof(bool),
          typeof(DataPoint),
          new PropertyMetadata(false, new PropertyChangedCallback(OnIsClickedByUserChanged)));

        public static readonly DependencyProperty ToolTipFormatProperty =
          DependencyProperty.Register("ToolTipFormat",
          typeof(string),
          typeof(DataPoint),
          new PropertyMetadata("", OnToolTipFormatChanged));

        public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem",
        typeof(object),
        typeof(DataPoint),
        new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));

        public static readonly DependencyProperty DataPointGroupIndexProperty =
        DependencyProperty.Register("DataPointGroupIndex",
        typeof(int),
        typeof(DataPoint),
        new PropertyMetadata(-1, new PropertyChangedCallback(OnSelectedItemChanged)));

        public static readonly DependencyProperty DataPointIndexProperty =
        DependencyProperty.Register("DataPointIndex",
        typeof(int),
        typeof(DataPoint),
        new PropertyMetadata(-1, new PropertyChangedCallback(OnSelectedItemChanged)));

        
        
        private static void OnIsClickedByUserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue) == true)
            {
                (d as DataPoint).UpdateSelection();
            }
        }

        private static void OnToolTipFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).UpdateDisplayProperties();
        }

        private void UpdateSelection()
        {
            SetValue(SelectedItemProperty, ReferencedObject);
        }



        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).SelectedItemChanged(e.NewValue);
        }

        public ChartBase ParentChart
        { get; private set; }

        public DataPoint(ChartBase parentChart)
        {
            ParentChart = parentChart;
        }

        private void SelectedItemChanged(object selectedObject)
        {
            if (selectedObject == ReferencedObject)
            {
                SetValue(IsSelectedProperty, true);
            }
            else
            {
                //IsSelected = false;
                SetValue(IsSelectedProperty, false);             
            }
        }

        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        /// <summary>
        /// Contains the absolute StartValue of the item depending on the values of the previous items values
        /// </summary>
        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set
            {
                SetValue(StartValueProperty, value);
            }
        }

        

        private static void SumOfDataPointGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).SumOfDataPointGroupChanged(double.Parse(e.NewValue.ToString()));
        }

        private void SumOfDataPointGroupChanged(double p)
        {
            RaisePropertyChangeEvent("PercentageFromSumOfDataPointGroup");
        }
        
        private static void MaxDataPointValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).MaxDataPointValueChanged(double.Parse(e.NewValue.ToString()));
        }

        private void MaxDataPointValueChanged(double p)
        {
            RaisePropertyChangeEvent("PercentageFromMaxDataPointValue");
            RaisePropertyChangeEvent("PercentageFromMaxNegativeDataPointValue");
        }

        public double PercentageFromSumOfDataPointGroup
        {
            get
            {
                if (SumOfDataPointGroup > 0)
                {
                    return Value / SumOfDataPointGroup;
                }
                return 0.0;
            }
        }        

        public double PercentageFromMaxDataPointValue
        {
            get
            {
               // if (MaxDataPointValue > 0)
                if (MaxPositiveGridLineValue > 0)
                {
                    
                   // return Value / MaxDataPointValue;
                    return Value / MaxPositiveGridLineValue;
                }
                return 0.0;
            }
        }

        /// <summary>
        /// Percentage from the largest negativeDataPOint (this should be % from the max Grid line)
        /// </summary>
        public double PercentageFromMaxNegativeDataPointValue
        {
            get
            {
                //if (MinDataPointValue < 0)
                if (MaxNegativeGridLineValue < 0)
                {
                   // return Value / MinDataPointValue;
                    return Value / MaxNegativeGridLineValue;
                }
                return 0.0;
            }
        }

        private static void MaxDataPointGroupSumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DataPoint).MaxDataPointGroupSumChanged(double.Parse(e.NewValue.ToString()));
        }

        private void MaxDataPointGroupSumChanged(double newMaxValue)
        {
            RaisePropertyChangeEvent("PercentageFromMaxDataPointGroupSum");
        }

        private void UpdatePercentage()
        {
            RaisePropertyChangeEvent("PercentageFromMaxDataPointGroupSum");
            RaisePropertyChangeEvent("PercentageFromMaxDataPointValue");
            RaisePropertyChangeEvent("PercentageFromSumOfDataPointGroup");
            RaisePropertyChangeEvent("PercentageFromMaxNegativeDataPointValue");
        }

        public double PercentageFromMaxDataPointGroupSum
        {
            get
            {
                if (MaxDataPointGroupSum > 0)
                {
                    return Value / MaxDataPointGroupSum;
                }
                return 0.0;
            }
        }

        /// <summary>
        /// Summe der Werte in meiner Gruppe
        /// </summary>
        public double SumOfDataPointGroup
        {
            get { return (double)GetValue(SumOfDataPointGroupProperty); }
            set { SetValue(SumOfDataPointGroupProperty, value); }
        }

        //GA added these - MaxDatapoint and MaxGridLIne can be differnt
        // max gridline is needed in calculations
        public double MaxPositiveGridLineValue
        {
            get { return (double)GetValue(MaxPositiveGridLineValueProperty); }
            set { SetValue(MaxPositiveGridLineValueProperty, value); }
        }

        public double MaxNegativeGridLineValue
        {
            get { return (double)GetValue(MaxNegativeGridLineValueProperty); }
            set { SetValue(MaxNegativeGridLineValueProperty, value); }
        }

        //GA added these - MaxDatapoint and MaxGridLIne can be differnt
        // max gridline is needed in calculations
        public Style GADataPointStyle
        {
            get { return (Style)GetValue(GADataPointStyleProperty); }
            set { SetValue(GADataPointStyleProperty, value); }
        }

        public Style GASelectedDataPointStyle
        {
            get { return (Style)GetValue(GASelectedDataPointStyleProperty); }
            set { SetValue(GASelectedDataPointStyleProperty, value); }
        }

        public Type GADataPointType
        {
            get { return (Type)GetValue(GADataPointTypeProperty); }
            set { SetValue(GADataPointTypeProperty, value); }
        }

        

        /// <summary>
        /// The last value the datapoint held (used to help redraw some charts)
        /// </summary>
        public double OldValue
        {
            get;
            set;
        }


        /// <summary>
        /// The location of the datapoint in the dataItem list  (used to help redraw some charts)
        /// </summary>
        public int DataPointIndex
        {
            get { return (int)GetValue(DataPointIndexProperty); }
            set { SetValue(DataPointIndexProperty, value); }
        }


        /// <summary>
        /// The location of the datapoint in the dataItem list  (used to help redraw some charts)
        /// </summary>
        public int DataPointGroupIndex
        {
            get { return (int)GetValue(DataPointGroupIndexProperty); }
            set { SetValue(DataPointGroupIndexProperty, value); }
        }


        /// <summary>
        /// Von außen wird dieser Wert gefüllt
        /// </summary>
        public double MaxDataPointValue
        {
            get { return (double)GetValue(MaxDataPointValueProperty); }
            set { SetValue(MaxDataPointValueProperty, value); }
        }

        public double MinDataPointValue
        {
            get { return (double)GetValue(MinDataPointValueProperty); }
            set { SetValue(MinDataPointValueProperty, value); }
        }

        public double MaxDataPointGroupSum
        {
            get { return (double)GetValue(MaxDataPointGroupSumProperty); }
            set { SetValue(MaxDataPointGroupSumProperty, value); }
        }

        public string ToolTipFormat
        {
            get { return (string)GetValue(ToolTipFormatProperty); }
            set { SetValue(ToolTipFormatProperty, value); }
        }

        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        

        public string SeriesCaption
        {
            get;
            set;
        }

        public Brush ItemBrush
        {
            get { return (Brush)GetValue(ItemBrushProperty); }
            set { SetValue(ItemBrushProperty, value); }
        }

        private object _ReferencedObject;

        public object ReferencedObject
        {
            get
            {
                return _ReferencedObject;
            }
            set
            {
                _ReferencedObject = value;
                UpdateDisplayProperties();
                if (_ReferencedObject is INotifyPropertyChanged)
                {
                    (_ReferencedObject as INotifyPropertyChanged).PropertyChanged += DataPoint_PropertyChanged;
                }
            }
        }


        private void UpdateDisplayProperties()
        {
            RaisePropertyChangeEvent("Value");
            RaisePropertyChangeEvent("FormattedValue");
            RaisePropertyChangeEvent("DisplayName");
        }               


        //get notified if value changes
        void DataPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ValueMember)
            {
                //raiseproperty change on value
                UpdateDisplayProperties();
                UpdatePercentage();
            }
            if (e.PropertyName == DisplayMember)
            {
                //raiseproperty change on displayname
            }
        }

        public string ValueMember { get; set; }

        public string DisplayMember { get; set; }

        public string DisplayName
        {
            get
            {
                if (_ReferencedObject == null)
                {
                    return "";
                }
                return GetItemValue(_ReferencedObject, DisplayMember);
            }
        }

        public string Caption
        {
            get
            {
                return DisplayName;
            }
        }

        public string FormattedValue
        {
            get
            {
                return string.Format(this.ToolTipFormat, this.Caption, this.Value, this.SeriesCaption, this.PercentageFromMaxDataPointGroupSum, this.PercentageFromMaxDataPointValue, this.PercentageFromSumOfDataPointGroup);
            }
        }

        public double Value
        {
            get
            {
                if (_ReferencedObject == null)
                {
                    return 0.0d;
                }
                return double.Parse(GetItemValue(_ReferencedObject, ValueMember).ToString());
            }
        }

        private string GetItemValue(object item, string propertyName)
        {
            if (item != null)
            {
                foreach (PropertyInfo info in item.GetType().GetAllProperties())
                {
                    if (info.Name == propertyName)
                    {
                        object v = info.GetValue(item, null);
                        return v.ToString();
                    }
                }
                throw new Exception(string.Format("Property '{0}' not found on item of type '{1}'", propertyName, item.GetType().ToString()));
            }
            return null;
        }

        private List<PropertyChangedEventHandler> addedHandlers = new List<PropertyChangedEventHandler>();

        private PropertyChangedEventHandler propChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                
                if (!addedHandlers.Contains(value))
                {
                    addedHandlers.Add(value);
                    propChanged += value; 
                }
                
            }
            remove
            {
                if (addedHandlers.Contains(value))
                {
                    addedHandlers.Remove(value);
                }
                propChanged -= value;
            }
        }

        



        public void clearAllPropertyChangedEvents()
        {
            this.propChanged = null;
        }

        private void RaisePropertyChangeEvent(String propertyName)
        {
            if (propChanged != null)
                this.propChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
