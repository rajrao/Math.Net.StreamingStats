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
        decimal? _variance;

        decimal _sum;
        decimal _reciprocalSum;
        private readonly Action<decimal>[] _mathFunctions;

        public StreamingStatsCollectorDecimal()
        {
            _mathFunctions = new Action<decimal>[]
            {
                CalcMinimum,
                CalcMaximum,
                CalcSum,
                CalcMean,
                CalcVariance
            };
        }

        #endregion  
        public void Collect(decimal value)
        {
            Count++;
            
            foreach (var mathFunc in _mathFunctions)
            {
                mathFunc(value);
            }
        }

        public ulong Count { get; private set; } = 0;
        public decimal? Minimum => Count > 0 ? _min : ((decimal?) null);
        public decimal? Maximum => Count > 0 ? _max : ((decimal?)null);
        public decimal? Range => Count > 0 ? _max - _min : ((decimal?)null);
        public decimal? Sum => Count > 0 ? _sum : ((decimal?)null);
        public decimal? Mean => Count > 0 ? _mean : ((decimal?)null);

        public decimal? PopulationVariance => Count > 1 ? _variance / Count : null;
        public double? PopulationStandardDeviation => PopulationVariance.HasValue ? System.Math.Sqrt((double)PopulationVariance) : (double?)null;

        public decimal? SampleVariance => Count > 1 ? _variance / (Count - 1) : null;
        public double? SampleStandardDeviation => SampleVariance.HasValue ? System.Math.Sqrt((double)SampleVariance) : (double?)null;

        public double? RootMeanSquare => Count > 1 ? System.Math.Sqrt((double)(_meanSquared)) : (double?)null;


        #region calculators

        private void CalcMinimum(decimal sample)
        {

            _min = System.Math.Min(sample, _min);
        }

        private void CalcMaximum(decimal sample)
        {
            _max = System.Math.Max(sample, _max);
        }

        private void CalcSum(decimal sample)
        {
            _sum += sample;
            _reciprocalSum += 1.0m / sample;
        }

        private void CalcMean(decimal sample)
        {
            _mean = _mean + ((sample - _mean) / Count);
            _meanSquared = _meanSquared + (((sample * sample) - _meanSquared) / Count);
        }

        private void CalcVariance(decimal sample)
        {
            if (Count <= 1) return;
            if (Count == 2)
            {
                _variance = 0;
            }

            decimal diff = (Count * sample) - _sum;
            _variance += (diff * diff) / (Count * (Count - 1));
        }


        #endregion
    }
}
