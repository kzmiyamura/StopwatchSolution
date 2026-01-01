// IStopwatch.cs
namespace StopwatchLogic
{
    public interface IStopwatch
    {
        void Start();
        void Stop();
        void Reset();
        double ElapsedSeconds { get; }
    }
}