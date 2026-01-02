using System;
using System.Diagnostics;
using System.Threading;

namespace StopwatchConsole
{
    class Program
    {
        static void Main()
        {
            // 高精度ストップウォッチ
            var stopwatch = new Stopwatch();

            // 最初にカーソル位置を固定
            Console.Clear();
            int left = 10;  // 水平方向の開始位置
            int top = 5;    // 垂直方向の開始位置
            Console.SetCursorPosition(left, top);

            stopwatch.Start();

            while (true)
            {
                var ts = stopwatch.Elapsed;

                // 表示文字列を固定幅に揃える
                string text = $"{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds / 100:D1}";
                text = text.PadLeft(7); // 常に7文字幅に揃える

                // 同じ位置に書き込む
                Console.SetCursorPosition(left, top);
                Console.Write(text);

                Thread.Sleep(10); // 0.01秒更新
            }
        }
    }
}
