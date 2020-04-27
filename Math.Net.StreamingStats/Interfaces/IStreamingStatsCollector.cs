using System;
using System.Collections.Generic;
using System.Text;

namespace Math.Net.StreamingStats.Interfaces
{

    /// <summary>
    /// Provides statistics on a list of values without actually storing the list of values
    /// The statistic information can be queried at any point in time during the capture of data and so
    /// can be useful to display progressive stats about the list.
    /// The formulas selected have been selected to minimize the possibility of overflow exceptions.
    /// </summary>
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

        /// <summary>
        /// Also called the Quadratic Mean
        /// </summary>
        double? RootMeanSquare { get; }

    }
}
