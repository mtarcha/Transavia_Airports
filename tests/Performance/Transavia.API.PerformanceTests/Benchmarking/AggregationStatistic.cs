using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Transavia.API.PerformanceTests.Benchmarking
{
    public class AggregationStatistic : IBenchmarkStatistic
    {
        private readonly ConcurrentBag<BenchmarkStatistic> _aggregationItems;

        public AggregationStatistic()
        {
            _aggregationItems = new ConcurrentBag<BenchmarkStatistic>();
        }

        public int RunsPerSecond { get; private set; }
        public TimeSpan AverageExecutionTime { get; private set; }

        public void EvaluateStatistics()
        {
            foreach (var aggregationItem in _aggregationItems)
            {
                aggregationItem.EvaluateStatistics();
            }

            RunsPerSecond = _aggregationItems.Select(x => x.RunsPerSecond).Sum();
            AverageExecutionTime = TimeSpan.FromMilliseconds(_aggregationItems.Select(x => x.AverageExecutionTime.Milliseconds).Average());
        }

        public void Aggregate(BenchmarkStatistic statistics)
        {
            _aggregationItems.Add(statistics);
        }
    }
}