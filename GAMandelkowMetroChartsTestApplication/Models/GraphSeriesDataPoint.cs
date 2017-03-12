using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace GAMandelkowMetroChartsTestApplication.Models
{
        public class GraphSeriesDataPoint : ViewModelBase
        {
            private string _date;
            public string date
            {
                get
                {
                    return _date;
                }
                set
                {
                    if (_date != value)
                    {
                        _date = value;
                        RaisePropertyChanged("date");
                    }
                }
            }

            private double _amount;
            public double amount
            {
                get
                {
                    return _amount;
                }
                set
                {
                    if (_amount != value)
                    {
                        _amount = value;
                        RaisePropertyChanged("amount");
                    }
                }
            }

            public GraphSeriesDataPoint(string date, double amount)
            {
                this.date = date;
                this.amount = amount;
            }
        }


    }

