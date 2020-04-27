using System;
using System.Collections.Generic;
using System.Text;
using Math.Net.StreamingStats.Interfaces;

namespace Math.Net.StreamingStats
{
    public class StreamingStatsCollectorDecimal : IStreamingStatsCollectorDecimal
    {

        #region privates
        decimal _min = decimal.MaxValue;
        decimal _max = decimal.MinValue;
        decimal _mean;
        decimal _meanSquared;
        decimal _meanSquared2;

        decimal _sum;
        
        private readonly Action<decimal>[] _mathFunctions;
        private object _lock = new object();

        public StreamingStatsCollectorDecimal()
        {
            _mathFunctions = new Action<decimal>[]
            {
                CalcMinimum,
                CalcMaximum,
                CalcSum,
                CalcMean,
            };
        }

        #endregion  
        public void Collect(decimal value)
        {
            lock (_lock)
            {
                Count++;

                foreach (var mathFunc in _mathFunctions)
                {
                    mathFunc(value);
                }
            }
        }

        public ulong Count { get; private set; }
        public decimal? Minimum => Count > 0 ? _min : (decimal?)null;
        public decimal? Maximum => Count > 0 ? _max : (decimal?)null;
        public decimal? Range => Count > 0 ? _max - _min : (decimal?)null;
        public decimal? Sum => Count > 0 ? _sum : (decimal?)null;
        public decimal? Mean => Count > 0 ? _mean : (decimal?)null;

        public decimal? PopulationVariance => Count > 1 ? _meanSquared / Count : (decimal?)null;
        public double? PopulationStandardDeviation => PopulationVariance.HasValue ? System.Math.Sqrt((double)PopulationVariance) : (double?)null;

        public decimal? SampleVariance => Count > 1 ? _meanSquared / (Count - 1) : (decimal?)null;
        public double? SampleStandardDeviation => SampleVariance.HasValue ? System.Math.Sqrt((double)SampleVariance) : (double?)null;

        public double? RootMeanSquare => Count > 1 ? System.Math.Sqrt((double)(_meanSquared2)) : (double?)null;


        #region calculators

        private void CalcMinimum(decimal newValue)
        {
            _min = System.Math.Min(newValue, _min);
        }

        private void CalcMaximum(decimal newValue)
        {
            _max = System.Math.Max(newValue, _max);
        }

        private void CalcSum(decimal newValue)
        {
            _sum += newValue;
        }

        private void CalcMean(decimal newValue)
        {
            //Mean calculation is based on the moving average calculations (Cumulative moving average)
            //https://en.wikipedia.org/wiki/Moving_average#Cumulative_moving_average
            //Variance calculation is based on Welford's algorithm to calculate the estimated Variance
            //https://en.wikipedia.org/wiki/Algorithms_for_calculating_variance#Welford's_online_algorithm
            var delta1 = (newValue - _mean);
            _mean = _mean + (delta1 / Count);
            var delta2 = (newValue - _mean);
            _meanSquared = _meanSquared + (delta2 * delta1);
            _meanSquared2 = _meanSquared2 + (((newValue * newValue) - _meanSquared2) / Count);

        }

        public override string ToString()
        {
            return ToString(" ", 4, OutputData.Basic | OutputData.Mean | OutputData.PopulationStats);
        }

        [Flags]
        public enum OutputData
        {
            Basic = 1,
            Mean = 2,
            PopulationStats = 4,
            SampleStats = 8,
            RootMeanSquare = 16,

            All = Int32.MaxValue,
        }

        public string ToString(string separator, int precision, OutputData outputData = OutputData.All)
        {
            string format = $"F{precision}";
            StringBuilder stringBuilder = new StringBuilder();
            if (outputData.HasFlag(OutputData.Basic))
            {
                stringBuilder.Append(
                    $"Count: {Count}{separator}Minimum:{Minimum?.ToString(format) ?? "NaN"}{separator}Maximum:{Maximum?.ToString(format) ?? "NaN"}{separator}Range:{Range?.ToString(format) ?? "NaN"}{separator}Sum:{Sum?.ToString(format) ?? "NaN"}{separator}");
            }

            if (outputData.HasFlag(OutputData.Mean))
            {
                stringBuilder.Append($"Mean:{Mean?.ToString(format) ?? "NaN"}{separator}");
            }
            if (outputData.HasFlag(OutputData.PopulationStats))
            {
                stringBuilder.Append($"Pop.Var: {PopulationVariance?.ToString(format) ?? "NaN"}{separator}Pop.Std.Dev:{PopulationStandardDeviation?.ToString(format) ?? "NaN"}{separator}");
            }
            if (outputData.HasFlag(OutputData.SampleStats))
            {
                stringBuilder.Append($"Sam.Var:{SampleVariance?.ToString(format) ?? "NaN"}{separator}Sam.Std.Dev:{SampleStandardDeviation?.ToString(format) ?? "NaN"}{separator}");
            }
            if (outputData.HasFlag(OutputData.RootMeanSquare))
            {
                stringBuilder.Append($"RMS:{RootMeanSquare?.ToString(format) ?? "NaN"}");
            }


            return stringBuilder.ToString();
        }

        #endregion
    }
}
