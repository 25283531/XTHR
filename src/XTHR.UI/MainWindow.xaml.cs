using System.Windows;
using XTHR.UI.Views;

namespace XTHR.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(CostAnalysisView costAnalysisView)
        {
            InitializeComponent();
            // This is a simple way to inject the view. 
            // A more robust solution might use a navigation service.
            this.Content = costAnalysisView;
        }
    }
}