using System;
using System.Collections.Generic;
using System.Linq;

namespace Transavia.API.PerformanceTests.Benchmarking
{
    public class BenchmarkStatistic : IBenchmarkStatistic
    {
        private readonly TimeSpan _totalExecutionTime;
        private readonly List<TimeSpan> _executions;

        public BenchmarkStatistic(TimeSpan totalExecutionTime)
        {
            _totalExecutionTime = totalExecutionTime;
            _executions = new List<TimeSpan>();
        }

        public int RunsPerSecond { get; private set; }

        public TimeSpan AverageExecutionTime { get; private set; }

        public void AddNewItem(TimeSpan executionTime)
        {
            _executions.Add(executionTime);
        }

        public void EvaluateStatistics()
        {
            RunsPerSecond = _executions.Count / _totalExecutionTime.Seconds;
            AverageExecutionTime = TimeSpan.FromTicks((long)_executions.Select(x => x.Ticks).Average());
        }
    }
}