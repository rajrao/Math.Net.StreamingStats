using System;
using System.Collections.Generic;
using System.Text;

namespace Math.Net.StreamingStats.Interfaces
{
    public interface IStreamingStatsCollectorDecimal
    {
        void Collect(decimal value);

        ulong Count { get; }

        decimal? Minimum { get; }

        decimal? Maximum { get; }

        decimal? Range { get; }

        decimal? Sum { get; }

        decimal? Mean { get; }

        decimal? PopulationVariance { get; }

        double? PopulationStandardDeviation { get; }

        decimal? SampleVariance { get; }
        
        double? SampleStandardDeviation { get;  }

        double? RootMeanSquare { get; }

    }
}
