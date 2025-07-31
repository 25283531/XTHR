using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace XTHR.UI.Controls
{
    public partial class CostAnalysisChart : UserControl
    {
        public CostAnalysisChart()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            Labels = new[] { "" };
            Formatter = value => value.ToString("C");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}