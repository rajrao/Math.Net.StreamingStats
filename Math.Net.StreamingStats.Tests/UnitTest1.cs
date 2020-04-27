using System;
using Math.Net.StreamingStats.Interfaces;
using Xunit;

namespace Math.Net.StreamingStats.Tests
{
    public class StreamingStatsCollectorDecimalTests
    {
        [Fact]
        public void WithoutSamplesReturnsCountOfZeroAndNulls()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            Assert.Equal(0,collector.Count);
            Assert.Null(collector.Maximum);
            Assert.Null(collector.Minimum);
            Assert.Null(collector.Range);
            Assert.Null(collector.Sum);
            Assert.Null(collector.Mean);
            Assert.Null(collector.Variance);
            Assert.Null(collector.StandardDeviation);
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
            Assert.Equal(0,collector.Variance);
            Assert.Equal(0,collector.StandardDeviation);
        }

        [Fact]
        public void WithSimpleSamplesReturnsValidValues()
        {
            IStreamingStatsCollectorDecimal collector = new StreamingStatsCollectorDecimal();

            collector.Collect(1);
            collector.Collect(2);
            collector.Collect(3);

            Assert.Equal(3, collector.Count);
            Assert.Equal(3, collector.Maximum);
            Assert.Equal(1, collector.Minimum);
            Assert.Equal(2, collector.Range);
            Assert.Equal(6, collector.Sum);
            Assert.Equal(2, collector.Mean);
            Assert.Equal(0.667m, collector.Variance);
            Assert.Equal(0.816m, collector.StandardDeviation);
        }
    }
}
