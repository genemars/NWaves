﻿namespace NWaves.Filters.Adaptive
{
    /// <summary>
    /// Class representing LMF Adaptive filter (Least-Mean-Fourth algorithm).
    /// </summary>
    public class LmfFilter : AdaptiveFilter
    {
        private readonly float _mu;
        private readonly float _leakage;

        /// <summary>
        /// Construct <see cref="LmfFilter"/> of given <paramref name="order"/>.
        /// </summary>
        /// <param name="order">Filter order</param>
        /// <param name="mu">Mu</param>
        /// <param name="leakage">Leakage</param>
        public LmfFilter(int order, float mu = 0.75f, float leakage = 0) : base(order)
        {
            _mu = mu;
            _leakage = leakage;
        }

        /// <summary>
        /// Process one sample of input signal and one sample of desired signal.
        /// </summary>
        /// <param name="input">Sample of input signal</param>
        /// <param name="desired">Sample of desired signal</param>
        public override float Process(float input, float desired)
        {
            var offset = _delayLineOffset;

            _delayLine[offset + _kernelSize] = input;   // duplicate it for better loop performance


            var y = Process(input);

            var e = desired - y;

            for (var i = 0; i < _kernelSize; i++, offset++)
            {
                _b[i] = _b[_kernelSize + i] = (1 - _leakage * _mu) * _b[i] + 4 * _mu * e * e * e * _delayLine[offset];
            }

            return y;
        }
    }
}
