using System;
using System.Collections.Generic;
using System.Text;

namespace Math.Net.StreamingStats.Interfaces
{
    public interface IStreamingStatsCollectorDecimal
    {
        void Collect(decimal value);

        long Count { get; }

        decimal? Minimum { get; }

        decimal? Maximum { get; }

        decimal? Range { get; }

        decimal? Sum { get; }

        decimal? Mean { get; }

        decimal? StandardDeviation { get; set; }

        decimal? Variance { get; set; }


    }
}
