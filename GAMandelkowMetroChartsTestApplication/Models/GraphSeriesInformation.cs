using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace GAMandelkowMetroChartsTestApplication.Models
{
    public class GraphSeriesInformation : ViewModelBase
    {
        private string _seriesDisplayName;
        public string seriesDisplayName
        {
            get
            {
                return _seriesDisplayName;
            }
            set
            {
                if (_seriesDisplayName != value)
                {
                    _seriesDisplayName = value;
                    RaisePropertyChanged("seriesDisplayName");
                }
            }
        }

        public ObservableCollection<GraphSeriesDataPoint> Items { get; set; }

        public GraphSeriesInformation()
        {
            Items = new ObservableCollection<GraphSeriesDataPoint>();
        }
    }
}