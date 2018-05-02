using System;

namespace MarxMachineLearning
{
    public class ActivationFunctions
    {
        public ActivationFunctions()
        {
        }

        public double[] Step(double[] _x, double _min, double _max)
        {
            double[] _rX = new double[_x.Length];

            for (int i = 0; i < _x.Length; i++)
            {
                if (_x[i] < ((_max - _min) / 2) + _min)
                    _rX[i] = _min;
                else
                    _rX[i] = _max;
            }

            return _rX;
        }

        public double[] TanSigmoid(double[] _x)
        {
            double[] _rX = new double[_x.Length];

            for(int i = 0; i < _x.Length; i++)
                _rX[i] = 2 / (1 + Math.Exp(-2 * _x[i])) - 1;

            return _rX;
        }

        public double[] LogSigmoid(double[] _x)
        {
            double[] _rX = new double[_x.Length];

            for (int i = 0; i < _x.Length; i++)
                _rX[i] = 1 / (1 + Math.Exp(-_x[i]));

            return _rX;
        }

        public double[] LogSigmoidBiased(double[] _x)
        {
            double[] _rX = new double[_x.Length];

            for (int i = 0; i < _x.Length; i++)
                _rX[i] = 1 / (1 + Math.Exp(-_x[i])) + 0.3;

            return _rX;
        }

        public double[] TruncatedLogSigmoid(double[] _x)
        {
            double[] _rX = new double[_x.Length];

            for (int i = 0; i < _x.Length; i++)
            {
                if (_x[i] < -45.0) _rX[i] = 0.0;
                else if (_x[i] > 45.0) _rX[i] = 1.0;
                else _rX[i] = 1.0 / (1.0 + Math.Exp(-_x[i]));
            }

            return _rX;
        }

        public double[] TruncatedHyperTanFunction(double[] _x)
        {
            double[] _rX = new double[_x.Length];

            for (int i = 0; i < _x.Length; i++)
            {
                if (_x[i] < -45.0) _rX[i] = - 1.0;
                else if (_x[i] > 45.0) _rX[i] = 1.0;
                else _rX[i] = Math.Tanh(_x[i]);
            }

            return _rX;
        }
    }
}
