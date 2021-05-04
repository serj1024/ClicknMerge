using System;
using System.Collections.Generic;
using System.Linq;

namespace MY.Scripts.Extensions
{
    internal class RandomWithoutRepeat
    {
        private HashSet<int> _numbers = new HashSet<int>();
        private Random _random;
        private int _maxNumbersCount;
        private int _minInclusive;
        private int _maxEclusive;
        public RandomWithoutRepeat(int minInclusive, int maxExclusive)
        {
            _random = new Random();
            _minInclusive = minInclusive;
            _maxEclusive = maxExclusive;
            _maxNumbersCount = maxExclusive - minInclusive;

            FillNumbers();
        }
        
        public int Next()
        {
            if(_numbers.Count == 0)
            {
                FillNumbers();
            }

            var value = _numbers.Last();
            _numbers.Remove(value);
            return value;
        }

        private void FillNumbers()
        {
            while (_numbers.Count < _maxNumbersCount)
            {
                _numbers.Add(_random.Next(_minInclusive, _maxEclusive));
            }
        }

    }
}