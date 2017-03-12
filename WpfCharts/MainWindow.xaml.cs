using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

// https://gamandelkowcharts.codeplex.com/
// http://latelierdundev.com/net-les-graphiques-partie-1-utilisation-generale/ http://igrali.com/2013/05/25/getting-started-with-free-metro-modern-ui-charts-windows-8/

namespace WpfCharts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ChartItem> listItems;
        private List<ChartItem> listItemsColonnes;

        public List<ChartItem> ListItems => listItems;

        public List<ChartItem> ListItemsColonnes => listItemsColonnes;


        private GraphSeriesInformation scatterData;

        public GraphSeriesInformation ScatterData => scatterData;


        private GraphSeriesInformation scatterData1;

        public GraphSeriesInformation ScatterData1 => scatterData1;


        private GraphSeriesInformation radialSeries;

        public GraphSeriesInformation RadialSeries => radialSeries;


        private GraphSeriesDataPoint selectedItem;

        public GraphSeriesDataPoint SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (value != null && selectedItem != value)
                {
                    selectedItem = value;
                }
            }
        }


        public MainWindow()
        {
           
            InitializeComponent();

            listItems = new List<ChartItem>
            {
                new ChartItem {Libelle = "Item 1", Valeur = 1},
                new ChartItem {Libelle = "Item 2", Valeur = 9},
                new ChartItem {Libelle = "Item 3", Valeur = 5},
                new ChartItem {Libelle = "Item 4", Valeur = 13},
            };

            listItemsColonnes = new List<ChartItem>
            {
                new ChartItem {Libelle = "Item 1", Valeur = 5},
                new ChartItem {Libelle = "Item 2", Valeur = 40},
                new ChartItem {Libelle = "Item 3", Valeur = 8},
                new ChartItem {Libelle = "Item 4", Valeur = 20},
            };

            // Example with negative values
            scatterData = new GraphSeriesInformation {SeriesDisplayName = "Series 1"};
            scatterData.Items.Add(new GraphSeriesDataPoint("lun", 1));
            scatterData.Items.Add(new GraphSeriesDataPoint("mar", -1));
            scatterData.Items.Add(new GraphSeriesDataPoint("mer", 2));
            scatterData.Items.Add(new GraphSeriesDataPoint("jeu", -2));
            scatterData.Items.Add(new GraphSeriesDataPoint("ven", 3));
            scatterData.Items.Add(new GraphSeriesDataPoint("sam", -3));
            scatterData.Items.Add(new GraphSeriesDataPoint("dim", 4));


            scatterData1 = new GraphSeriesInformation {SeriesDisplayName = "Series 2"};
            scatterData1.Items.Add(new GraphSeriesDataPoint("lun", -1));
            scatterData1.Items.Add(new GraphSeriesDataPoint("mar", -2));
            scatterData1.Items.Add(new GraphSeriesDataPoint("mer", 4));
            scatterData1.Items.Add(new GraphSeriesDataPoint("jeu", -7));
            scatterData1.Items.Add(new GraphSeriesDataPoint("ven", -2.8));
            scatterData1.Items.Add(new GraphSeriesDataPoint("sam", 2));
            scatterData1.Items.Add(new GraphSeriesDataPoint("dim", -5));

            radialSeries = new GraphSeriesInformation { SeriesDisplayName = "Value" };
            radialSeries.Items.Add(new GraphSeriesDataPoint("Percent finished", 75));


            DataContext = this;
        }
    }

    public class ChartItem
    {
        public string Libelle { get; set; }

        public int Valeur { get; set; }
    }


    public class GraphSeriesInformation
    {
        private string seriesDisplayName;

        public string SeriesDisplayName
        {
            get { return seriesDisplayName; }
            set
            {
                if (value != null && seriesDisplayName != value)
                {
                    seriesDisplayName = value;
                }
            }
        }

        public ObservableCollection<GraphSeriesDataPoint> Items { get; set; }

        public GraphSeriesInformation()
        {
            Items = new ObservableCollection<GraphSeriesDataPoint>();
        }
    }


    public class GraphSeriesDataPoint
    {
        private string date;

        public string Date
        {
            get { return date; }
            set
            {
                if (value != null && date != value)
                {
                    date = value;
                }
            }
        }

        private double amount;

        public double Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                }
            }
        }

        public GraphSeriesDataPoint(string date, double amount)
        {
            this.Date = date;
            this.Amount = amount;
        }
    }
}