using System.Windows;
using StopwatchWpf.ViewModels;

namespace StopwatchWpf
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
