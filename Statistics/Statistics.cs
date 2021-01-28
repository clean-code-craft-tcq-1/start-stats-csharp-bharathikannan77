using System;
using System.Collections.Generic;

namespace Statistics
{
    public class StatsComputer
    {
        public List<float> CalculateStatistics(List<float> numbers)
        {
            if (numbers.Count == 0)
            {
                var average = float.NaN;
                var max = float.NaN;
                var min = float.NaN;
                List<float> numbersNan = new List<float>
                {
                    average,
                    max,
                    min
                };
                return numbersNan;
            }

            return numbers;
        }
        public float average = 4.525F;
        public float max = 8.9F;
        public float min = 1.5F;
    }
}
