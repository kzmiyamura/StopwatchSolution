using System.Diagnostics;

namespace StopwatchLogic
{
    /// <summary>
    /// Stopwatch クラスを使用した高精度なストップウォッチ実装
    /// </summary>
    public class HighPrecisionStopwatchV2 : IStopwatch
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// IStopwatchインターフェースで定義されたプロパティの実装
        /// </summary>
        public double ElapsedSeconds => _stopwatch.Elapsed.TotalSeconds;

        /// <summary>
        /// 計測を開始します
        /// </summary>
        public void Start()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// 計測を停止します
        /// </summary>
        public void Stop()
        {
            _stopwatch.Stop();
        }

        /// <summary>
        /// 計測をリセットし、停止状態にします
        /// </summary>
        public void Reset()
        {
            _stopwatch.Reset();
        }

        /// <summary>
        /// (互換用メソッド) 経過秒数を取得します
        /// </summary>
        public double GetElapsedSeconds()
        {
            return ElapsedSeconds;
        }
    }
}