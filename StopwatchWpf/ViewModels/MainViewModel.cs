using StopwatchLogic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace StopwatchWpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IStopwatch _stopwatch;
        private readonly DispatcherTimer _timer;

        private string _elapsedText = "00:00.0";
        public string ElapsedText
        {
            get => _elapsedText;
            private set
            {
                _elapsedText = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }

        public MainViewModel()
        {
            _stopwatch = new HighPrecisionStopwatchV2();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += (_, _) => UpdateTime();

            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            ResetCommand = new RelayCommand(Reset);
        }

        private void Start()
        {
            _stopwatch.Start();
            _timer.Start();
        }

        private void Stop()
        {
            _stopwatch.Stop();
            _timer.Stop();
            UpdateTime();
        }

        private void Reset()
        {
            _stopwatch.Reset();
            UpdateTime();
        }

        private void UpdateTime()
        {
            ElapsedText = FormatTime(_stopwatch.ElapsedSeconds);
        }

        private static string FormatTime(double seconds)
        {
            var time = TimeSpan.FromSeconds(seconds);
            return $"{time.Minutes:D2}:{time.Seconds:D2}.{time.Milliseconds / 100:D1}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
