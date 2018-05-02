using System;

namespace MarxMachineLearning
{
    public class NeuralNetwork
    {
        public NeuralNetwork()
        {
        }

        public double[] PerceptronLayer(int _numberOfNetworks, double[] _input, double[] _Weights, int _numberOfInputs, double[] _bias)
        {
            double[] _sum = new double[_numberOfNetworks * (_input.Length / _numberOfInputs)];

            int _wIndex = 0;
            int _sIndex = 0;
            int _iIndex = 0;
            bool _shouldLoopWeights = (_input.Length > _Weights.Length == true) ? true : false;

            for (int k = 0; k < _numberOfNetworks; k++)
            {
                for (int i = 0; i < _input.Length / _numberOfInputs; i++)
                {
                    for (int j = 0; j < _numberOfInputs; j++)
                    {
                        _sum[_sIndex] += (_input[_iIndex] * _Weights[_wIndex]);
                        _wIndex++;
                        _iIndex++;
                    }

                    _sum[_sIndex] += _bias[_sIndex];

                    if (_shouldLoopWeights)
                        _wIndex = 0;

                    _sIndex++;
                }
                _iIndex = 0;
            }

            return _sum;
        }
    }
}