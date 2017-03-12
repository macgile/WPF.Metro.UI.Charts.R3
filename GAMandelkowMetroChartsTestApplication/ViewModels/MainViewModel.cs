using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GAMandelkowMetroChartsTestApplication.Models;

namespace GAMandelkowMetroChartsTestApplication.ViewModels
{
    /// <summary>
    /// *****************************
    /// NOTE : Debug / enable the VS hosting process can be turned off to maintain the config file
    /// This is in the project properties / debig tab
    /// so that when testing you dont have to keep recreating the DB
    /// ALSO see the TransactionContext : DbContext to ensure the createAlways or only if required is set!
    /// ******************************
    /// 
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private GraphSeriesInformation _scatterData;

        public GraphSeriesInformation scatterData
        {
            get { return _scatterData; }
            set
            {
                if (_scatterData != value)
                {
                    _scatterData = value;
                    RaisePropertyChanged("scatterData");
                }
            }
        }

        private GraphSeriesInformation _scatterData1;

        public GraphSeriesInformation scatterData1
        {
            get { return _scatterData1; }
            set
            {
                if (_scatterData1 != value)
                {
                    _scatterData1 = value;
                    RaisePropertyChanged("scatterData1");
                }
            }
        }

        private GraphSeriesInformation _scatterData2;

        public GraphSeriesInformation scatterData2
        {
            get { return _scatterData2; }
            set
            {
                if (_scatterData2 != value)
                {
                    _scatterData2 = value;
                    RaisePropertyChanged("scatterData2");
                }
            }
        }

        private GraphSeriesInformation _scatterData3;

        public GraphSeriesInformation scatterData3
        {
            get { return _scatterData3; }
            set
            {
                if (_scatterData3 != value)
                {
                    _scatterData3 = value;
                    RaisePropertyChanged("scatterData3");
                }
            }
        }

        private GraphSeriesInformation _radialSeries;

        public GraphSeriesInformation radialSeries
        {
            get { return _radialSeries; }
            set
            {
                if (_radialSeries != value)
                {
                    _radialSeries = value;
                    RaisePropertyChanged("radialSeries");
                }
            }
        }

        private GraphSeriesDataPoint _selectedItem;

        public GraphSeriesDataPoint selectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged("selectedItem");
                }
            }
        }

        private bool _seriesSwitched;

        public bool seriesSwitched
        {
            get { return _seriesSwitched; }
            set
            {
                if (_seriesSwitched != value)
                {
                    _seriesSwitched = value;
                    RaisePropertyChanged("seriesSwitched");
                }
            }
        }

        public RelayCommand SwitchSeries { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
            }


            SwitchSeries = new RelayCommand(switchSeries);
            seriesSwitched = false;

            scatterData = new GraphSeriesInformation {seriesDisplayName = "Series 1"};
            scatterData.Items.Add(new GraphSeriesDataPoint("mon", 1));
            scatterData.Items.Add(new GraphSeriesDataPoint("tue", -1));
            scatterData.Items.Add(new GraphSeriesDataPoint("wed", 2));
            scatterData.Items.Add(new GraphSeriesDataPoint("thur", -2));
            scatterData.Items.Add(new GraphSeriesDataPoint("fri", 3));
            scatterData.Items.Add(new GraphSeriesDataPoint("sat", -3));
            scatterData.Items.Add(new GraphSeriesDataPoint("sun", 4));


            scatterData1 = new GraphSeriesInformation {seriesDisplayName = "Series 2"};
            scatterData1.Items.Add(new GraphSeriesDataPoint("mon", -1));
            scatterData1.Items.Add(new GraphSeriesDataPoint("tue", -2));
            scatterData1.Items.Add(new GraphSeriesDataPoint("wed", -4.0));
            scatterData1.Items.Add(new GraphSeriesDataPoint("thur", -9));
            scatterData1.Items.Add(new GraphSeriesDataPoint("fri", -2.8));
            scatterData1.Items.Add(new GraphSeriesDataPoint("sat", -3));
            scatterData1.Items.Add(new GraphSeriesDataPoint("sun", -5));

            Debug.WriteLine(scatterData1.Items.Count);



            scatterData2 = new GraphSeriesInformation {seriesDisplayName = "Series 3"};
            scatterData2.Items.Add(new GraphSeriesDataPoint("mon", 10));
            scatterData2.Items.Add(new GraphSeriesDataPoint("tue", 23));
            scatterData2.Items.Add(new GraphSeriesDataPoint("wed", 20));
            scatterData2.Items.Add(new GraphSeriesDataPoint("thur", 19));
            scatterData2.Items.Add(new GraphSeriesDataPoint("fri", 7));
            scatterData2.Items.Add(new GraphSeriesDataPoint("sat", 5));
            scatterData2.Items.Add(new GraphSeriesDataPoint("sun", 10));

            scatterData3 = new GraphSeriesInformation {seriesDisplayName = "Series 4"};
            scatterData3.Items.Add(new GraphSeriesDataPoint("mon", 1));
            scatterData3.Items.Add(new GraphSeriesDataPoint("tue", -6));
            scatterData3.Items.Add(new GraphSeriesDataPoint("wed", 18));
            scatterData3.Items.Add(new GraphSeriesDataPoint("thur", -8));
            scatterData3.Items.Add(new GraphSeriesDataPoint("fri", 0));
            scatterData3.Items.Add(new GraphSeriesDataPoint("sat", -6));
            scatterData3.Items.Add(new GraphSeriesDataPoint("sun", 20));


            radialSeries = new GraphSeriesInformation {seriesDisplayName = "Value"};
            radialSeries.Items.Add(new GraphSeriesDataPoint("Percent finished", 75));
        }

        private void switchSeries()
        {
            seriesSwitched = !seriesSwitched;
        }
    }
}