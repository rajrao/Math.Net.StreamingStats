using System;
using System.Collections.Generic;
using System.Text;
using Math.Net.StreamingStats.Interfaces;

namespace Math.Net.StreamingStats
{
    public class StreamingStatsCollectorDecimal : IStreamingStatsCollectorDecimal
    {
        public void Collect(decimal value)
        {
            throw new NotImplementedException();
        }

        public long Count { get; }
        public decimal? Minimum { get; }
        public decimal? Maximum { get; }
        public decimal? Range { get; }
        public decimal? Sum { get; }
        public decimal? Mean { get; }
        public decimal? StandardDeviation { get; set; }
        public decimal? Variance { get; set; }
    }
}
