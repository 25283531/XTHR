using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Services;

namespace XTHR.UI.ViewModels
{
    public class CostAnalysisViewModel : ViewModelBase
    {
        private readonly IPayrollQueryService _payrollQueryService;
        private DateTime _startDate = DateTime.Now.AddYears(-1);
        private DateTime _endDate = DateTime.Now;
        private CostAnalysisDimension _selectedDimension;
        private SeriesCollection _seriesCollection;
        private string[] _labels;

        public CostAnalysisViewModel(IPayrollQueryService payrollQueryService)
        {
            _payrollQueryService = payrollQueryService;
            LoadChartCommand = new RelayCommand(async () => await LoadChartData());
            Dimensions = new ObservableCollection<CostAnalysisDimension>(Enum.GetValues(typeof(CostAnalysisDimension)).Cast<CostAnalysisDimension>());
            SelectedDimension = Dimensions.First();
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public ObservableCollection<CostAnalysisDimension> Dimensions { get; }

        public CostAnalysisDimension SelectedDimension
        {
            get => _selectedDimension;
            set => SetProperty(ref _selectedDimension, value);
        }

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set => SetProperty(ref _seriesCollection, value);
        }

        public string[] Labels
        {
            get => _labels;
            set => SetProperty(ref _labels, value);
        }

        public ICommand LoadChartCommand { get; }

        private async Task LoadChartData()
        {
            var data = await _payrollQueryService.GetCostAnalysisAsync(StartDate, EndDate, SelectedDimension);

            Labels = data.Select(d => d.Dimension).ToArray();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "应发工资",
                    Values = new ChartValues<decimal>(data.Select(d => d.TotalGrossPay))
                },
                new ColumnSeries
                {
                    Title = "实发工资",
                    Values = new ChartValues<decimal>(data.Select(d => d.TotalNetPay))
                }
            };
        }
    }
}