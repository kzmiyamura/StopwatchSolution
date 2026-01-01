// IStopwatch.cs
namespace StopwatchConsole.StopwatchLogic
{
    public interface IStopwatch
    {
        void Start();
        void Stop();
        void Reset();
        double ElapsedSeconds { get; }
    }
}