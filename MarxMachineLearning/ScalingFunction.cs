using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarxMachineLearning
{
    public struct MinMax
    {
        public double _min;
        public double _max;
    }

    public class ScalingFunction
    {
        public ScalingFunction()
        {
        }

        public MinMax FindMinMax(double[] _input)
        {
            MinMax _minMax = new MinMax();
            _minMax._min = double.MaxValue;
            _minMax._max = double.MinValue;

            for (int i = 0; i < _input.Length; i++)
            {
                if (_input[i] > _minMax._max)
                    _minMax._max = _input[i];

                if (_input[i] < _minMax._min)
                    _minMax._min = _input[i];
            }

            return _minMax;
        }

        public double[] LinearScaleToRange(double[] _input, MinMax _oldMinMax, MinMax _newMinMax)
        {
            double[] _scaled = new double[_input.Length];
            double _oldMinMaxDiff = _oldMinMax._max - _oldMinMax._min;

            for (int i = 0; i < _input.Length; i++)
            {
                double _inputOldMinDiff = _input[i] - _oldMinMax._min;
                _scaled[i] = (_newMinMax._min * (1 - (_inputOldMinDiff / _oldMinMaxDiff))) + (_newMinMax._max * (_inputOldMinDiff / _oldMinMaxDiff));
            }

            return _scaled;
        }
    }
}
