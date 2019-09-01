using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Transavia.API.PerformanceTests.Benchmarking
{
    public class BenchmarkRunner
    {
        public static IBenchmarkStatistic Run(Action test, int warmupIterations, int maxDegreeOfParallelism)
        {
            Warmup(test, warmupIterations);

            var aggregatedStatistic = new AggregationStatistic();

            Parallel.For(
                0,
                maxDegreeOfParallelism,
                new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism },
                () => new InitialState(TimeSpan.FromSeconds(10)),
                (i, y, state) =>
                {
                    var totalExecutionTime = state.TotalExecutionTime;
                    var statistic = state.Statistic;
                    var totalCounter = state.TotalExecutionTimeStopwatch;
                    var testCounter = state.InvokeExecutionTimeStopwatch;

                    totalCounter.Start();
                    while (totalCounter.ElapsedTicks < totalExecutionTime.Ticks)
                    {
                        testCounter.Restart();
                        test();
                        statistic.AddNewItem(testCounter.Elapsed);
                    }

                    return state;
                },
                x => aggregatedStatistic.Aggregate(x.Statistic));

            aggregatedStatistic.EvaluateStatistics();

            return aggregatedStatistic;
        }

        private static void Warmup(Action test, int warmupIterations)
        {
            for (var i = 0; i < warmupIterations; i++)
            {
                test();
            }
        }

        private class InitialState
        {
            public InitialState(TimeSpan totalExecutionTime)
            {
                TotalExecutionTime = totalExecutionTime;
                Statistic = new BenchmarkStatistic(totalExecutionTime);
                TotalExecutionTimeStopwatch = new Stopwatch();
                InvokeExecutionTimeStopwatch = new Stopwatch();
            }

            public TimeSpan TotalExecutionTime { get; }
            public BenchmarkStatistic Statistic { get; }
            public Stopwatch TotalExecutionTimeStopwatch { get; }
            public Stopwatch InvokeExecutionTimeStopwatch { get; }
        }
    }
}