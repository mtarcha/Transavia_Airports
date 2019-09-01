using System;

namespace Transavia.API.PerformanceTests.Benchmarking
{
    public interface IBenchmarkStatistic
    {
        int RunsPerSecond { get; }

        TimeSpan AverageExecutionTime { get; }

        void EvaluateStatistics();
    }
}