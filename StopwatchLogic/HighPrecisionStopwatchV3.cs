using System;
using System.Diagnostics;
using System.Threading;

namespace StopwatchLogic
{
    public class HighPrecisionStopwatchV3 : IStopwatch
    {
        private readonly Stopwatch _stopwatch = new();
        private Thread? _worker;
        private volatile bool _running;

        public double ElapsedSeconds { get; private set; }

        // ← ここを追加
        public event Action? ElapsedChanged;

        public void Start()
        {
            if (_running) return;

            _running = true;
            _stopwatch.Start();

            _worker = new Thread(Loop)
            {
                IsBackground = true
            };
            _worker.Start();
        }

        public void Stop()
        {
            _running = false;
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _running = false;
            _stopwatch.Reset();
            ElapsedSeconds = 0;
            ElapsedChanged?.Invoke(); // リセット時も通知
        }

        private void Loop()
        {
            while (_running)
            {
                ElapsedSeconds = _stopwatch.Elapsed.TotalSeconds;
                ElapsedChanged?.Invoke(); // 値が変わったら通知
                Thread.Sleep(10); // 精度と負荷のバランス
            }
        }
    }
}
