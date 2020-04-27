using System;
using Math.Net.StreamingStats.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace Math.Net.StreamingStats.Tests
{
    public class StreamingStatsCollectorDecimalTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public StreamingStatsCollectorDecimalTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void WithoutSamplesReturnsCountOfZeroAndNulls()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            Assert.Equal(0ul,collector.Count);
            Assert.Null(collector.Maximum);
            Assert.Null(collector.Minimum);
            Assert.Null(collector.Range);
            Assert.Null(collector.Sum);
            Assert.Null(collector.Mean);
            Assert.Null(collector.PopulationVariance);
            Assert.Null(collector.PopulationStandardDeviation);
            Assert.Null(collector.SampleVariance);
            Assert.Null(collector.SampleStandardDeviation);
        }

        [Fact]
        public void WithSingleSampleReturnsValidValues()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            decimal value = 1;
            collector.Collect(value);


            Assert.Equal(value, collector.Count);
            Assert.Equal(value,collector.Maximum);
            Assert.Equal(value, collector.Minimum);
            Assert.Equal(0, collector.Range);
            Assert.Equal(value, collector.Sum);
            Assert.Equal(value, collector.Mean);
            Assert.Null(collector.PopulationVariance);
            Assert.Null(collector.PopulationStandardDeviation);
            Assert.Null(collector.SampleVariance);
            Assert.Null(collector.SampleStandardDeviation);
        }

        [Fact]
        public void WithSimpleSamplesReturnsValidValues()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            foreach (var val in new []{1,2,3,3,9,10})
            {
                collector.Collect(val);
                _testOutputHelper.WriteLine(collector.ToString());
            }

           
            
            Assert.Equal(6ul, collector.Count);
            Assert.Equal(10, collector.Maximum);
            Assert.Equal(1, collector.Minimum);
            Assert.Equal(9, collector.Range);
            Assert.Equal(28, collector.Sum);
            Assert.Equal(4.667m, collector.Mean.Value,3);
            Assert.Equal(12.222m, collector.PopulationVariance.Value,3);
            Assert.Equal(3.496, collector.PopulationStandardDeviation.Value,3);
            Assert.Equal(14.667m, collector.SampleVariance.Value,3);
            Assert.Equal(3.8297, collector.SampleStandardDeviation.Value,4);
            Assert.Equal(5.83095, collector.RootMeanSquare.Value, 5);
        }

        [Fact]
        public void WithSimpleNegativeSamplesReturnsValidValues()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            foreach (var val in new[] { -1, -2, -3, -3, -9, -10 })
            {
                collector.Collect(val);
                _testOutputHelper.WriteLine(collector.ToString());
            }

            Assert.Equal(6ul, collector.Count);
            Assert.Equal(-1, collector.Maximum);
            Assert.Equal(-10, collector.Minimum);
            Assert.Equal(9, collector.Range);
            Assert.Equal(-28, collector.Sum);
            Assert.Equal(-4.667m, collector.Mean.Value, 3);
            Assert.Equal(12.222m, collector.PopulationVariance.Value, 3);
            Assert.Equal(3.496, collector.PopulationStandardDeviation.Value, 3);
            Assert.Equal(14.667m, collector.SampleVariance.Value, 3);
            Assert.Equal(3.8297, collector.SampleStandardDeviation.Value, 4);
            Assert.Equal(5.83095, collector.RootMeanSquare.Value, 5);
        }
    }
}
