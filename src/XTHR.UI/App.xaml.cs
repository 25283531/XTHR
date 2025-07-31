using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Repositories;
using XTHR.Core.Interfaces.Services;
using XTHR.Data.Services;
using XTHR.UI.ViewModels;
using XTHR.UI.Views;

namespace XTHR.UI
{
    /// <summary>
    /// XTHR工资计算软件主应用程序类
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Repositories
            services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IEmployeeFinancialAdjustmentRepository, EmployeeFinancialAdjustmentRepository>();
            services.AddSingleton<IEmployeeSalaryComponentRepository, EmployeeSalaryComponentRepository>();
            services.AddSingleton<IPayrollResultRepository, PayrollResultRepository>();
            services.AddSingleton<IPayrollResultDetailRepository, PayrollResultDetailRepository>();
            services.AddSingleton<IPayrollRuleRepository, PayrollRuleRepository>();
            services.AddSingleton<IPerformanceReviewRepository, PerformanceReviewRepository>();
            services.AddSingleton<ISalaryComponentRepository, SalaryComponentRepository>();
            services.AddSingleton<ISocialSecurityAccountRepository, SocialSecurityAccountRepository>();
            services.AddSingleton<ISocialSecurityItemRepository, SocialSecurityItemRepository>();
            services.AddSingleton<IAttendanceRecordRepository, AttendanceRecordRepository>();

            // Services
            services.AddTransient<IPayrollCalculationService, PayrollCalculationService>();
            services.AddTransient<IPayrollQueryService, PayrollQueryService>();
            services.AddTransient<IPayrollRuleService, PayrollRuleService>();

            // ViewModels
            services.AddTransient<CostAnalysisViewModel>();

            // Views
            services.AddTransient<MainWindow>();
            services.AddTransient<CostAnalysisView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }    private IHost? _host;

        /// <summary>
        /// 应用程序启动时的初始化
        /// </summary>
        /// <param name="e">启动事件参数</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // 配置日志
                ConfigureLogging();

                // 创建主机并配置依赖注入
                _host = CreateHostBuilder(e.Args).Build();

                // 启动主机
                _host.Start();

                // 初始化数据库
                InitializeDatabase();

                // 显示主窗口
                var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用程序启动失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }

        /// <summary>
        /// 应用程序退出时的清理
        /// </summary>
        /// <param name="e">退出事件参数</param>
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _host?.Dispose();
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                // 记录退出时的异常，但不阻止应用程序退出
                System.Diagnostics.Debug.WriteLine($"应用程序退出时发生异常：{ex.Message}");
            }
            finally
            {
                base.OnExit(e);
            }
        }

        /// <summary>
        /// 创建主机构建器并配置服务
        /// </summary>
        /// <param name="args">命令行参数</param>
        /// <returns>主机构建器</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((context, services) =>
                {
                    // 注册数据访问层服务
                    services.AddSingleton<IDatabaseService, DatabaseService>();
                    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                    services.AddScoped<ISalaryBaseRepository, SalaryBaseRepository>();
                    services.AddScoped<IPerformanceRepository, PerformanceRepository>();
                    services.AddScoped<ISocialSecurityRepository, SocialSecurityRepository>();
                    services.AddScoped<IAttendanceRepository, AttendanceRepository>();
                    services.AddScoped<IPayrollRepository, PayrollRepository>();

                    // 注册业务逻辑层服务
                    services.AddScoped<IEmployeeService, EmployeeService>();
                    services.AddScoped<IImportService, ImportService>();
                    services.AddScoped<IPayrollCalculationService, PayrollCalculationService>();
                    services.AddScoped<IReportService, ReportService>();
                    services.AddScoped<IConfigurationService, ConfigurationService>();

                    // 注册视图模型
                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<EmployeeManagementViewModel>();
                    services.AddTransient<ImportDataViewModel>();
                    services.AddTransient<PayrollCalculationViewModel>();
                    services.AddTransient<ReportAnalysisViewModel>();
                    services.AddTransient<SettingsViewModel>();

                    // 注册视图
                    services.AddTransient<MainWindow>();
                    services.AddTransient<EmployeeManagementWindow>();
                    services.AddTransient<ImportDataWindow>();
                    services.AddTransient<PayrollCalculationWindow>();
                    services.AddTransient<ReportAnalysisWindow>();
                    services.AddTransient<SettingsWindow>();
                });

        /// <summary>
        /// 配置日志系统
        /// </summary>
        private static void ConfigureLogging()
        {
            // 确保日志目录存在
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            Directory.CreateDirectory(logPath);

            // 配置Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(
                    path: Path.Combine(logPath, "xthr-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("XTHR工资计算软件启动");
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                var databaseService = _host?.Services.GetRequiredService<IDatabaseService>();
                databaseService?.InitializeDatabase();
                Log.Information("数据库初始化完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "数据库初始化失败");
                throw;
            }
        }

        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">异常事件参数</param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Error(e.Exception, "发生未处理的异常");
            
            MessageBox.Show(
                $"应用程序发生未处理的异常：\n{e.Exception.Message}\n\n详细信息已记录到日志文件中。",
                "错误",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            e.Handled = true;
        }

        /// <summary>
        /// 应用程序域未处理异常
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="e">异常事件参数</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Log.Fatal(ex, "发生致命异常，应用程序即将退出");
            }
        }
    }
}