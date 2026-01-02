using Microsoft.Extensions.Options;
using StopwatchLogic;
using StopwatchWpf.Settings;
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
        private readonly Dispatcher _uiDispatcher;

        private string _elapsedText = "00:00.00"; // 0.01 秒まで表示
        public string ElapsedText
        {
            get => _elapsedText;
            private set
            {
                if (_elapsedText == value) return; // 変更なしなら無視
                _elapsedText = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand ResetCommand { get; }

        public MainViewModel(
            IStopwatch stopwatch,
            IOptions<StopwatchSettings>? settings = null) // optional for DI
        {
            _stopwatch = stopwatch;
            _uiDispatcher = Dispatcher.CurrentDispatcher;

            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            ResetCommand = new RelayCommand(Reset);

            if (_stopwatch is HighPrecisionStopwatchV3 hp)
            {
                hp.ElapsedChanged += () =>
                {
                    // UI スレッドで安全に更新
                    _uiDispatcher.Invoke(() =>
                    {
                        ElapsedText = FormatTime(hp.ElapsedSeconds);
                    });
                };
            }
        }

        private void Start() => _stopwatch.Start();
        private void Stop() => _stopwatch.Stop();
        private void Reset()
        {
            _stopwatch.Reset();
            ElapsedText = "00:00.00";
        }

        private static string FormatTime(double seconds)
        {
            var time = TimeSpan.FromSeconds(seconds);
            return $"{time.Minutes:D2}:{time.Seconds:D2}.{time.Milliseconds / 10:D2}";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
