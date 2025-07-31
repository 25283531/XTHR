using System;
using System.ComponentModel;
using System.Windows.Input;

namespace XTHR.UI.ViewModels
{
    public class DataImportViewModel : INotifyPropertyChanged
    {
        private string _statusMessage;

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public ICommand ImportDataCommand { get; }

        public DataImportViewModel()
        {
            ImportDataCommand = new RelayCommand(ImportData);
        }

        private void ImportData(object obj)
        {
            try
            {
                // 在这里实现数据导入逻辑
                // 例如，打开文件对话框，读取文件内容，然后将数据保存到数据库

                StatusMessage = "数据导入成功！";
            }
            catch (Exception ex)
            {
                // 记录异常信息
                StatusMessage = $"数据导入失败：{ex.Message}";
            }
        }
    {
        

                public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand 的一个简单实现
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}