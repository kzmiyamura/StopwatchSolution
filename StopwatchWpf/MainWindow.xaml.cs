using System.Windows;
using StopwatchWpf.ViewModels;
using StopwatchLogic;
using Microsoft.Extensions.Options;
using StopwatchWpf.Settings;

namespace StopwatchWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow(IStopwatch stopwatch, IOptions<StopwatchSettings> settings)
        {
            InitializeComponent();

            // ViewModel に依存性注入されたパラメータを渡す
            DataContext = new MainViewModel(stopwatch, settings);
        }
    }
}
