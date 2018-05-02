using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarxMachineLearning
{
    public struct UpdateParams
    {
        public double[] _weights;
        public double[] _delta;
    }
    public class BackPropagation
    {
        public BackPropagation()
        {
        }

        //Output
        public double[] GradientLogSigmoid(double[] _outputs, double[] _targetOutputs)
        {
            double[] _outputGradients = new double[_outputs.Length];

            for (int i = 0; i < _outputGradients.Length; i++)
            {
                double derivative = (1 - _outputs[i]) * _outputs[i]; // derivative of log-sigmoid is y(1-y) - formula dependent on shape
                _outputGradients[i] = derivative * (_targetOutputs[i] - _outputs[i]); // oGrad = (1 - O)(O) * (T-O) - x, y move control - area
            }

            return _outputGradients;
        }

        //Hidden
        public double[] GradientLogSigmoid(double[] _outputs, double[] _weights, double[] _nextLayerGradients)
        {
            double[] _outputGradients = new double[_outputs.Length];

            int _index = 0;
            for (int i = 0; i < _outputGradients.Length; i++)
            {
                //double derivative = (1 - _outputs[i]) * _outputs[i]; // derivative of log-sigmoid is y(1-y) - formula dependent on shape
                double derivative = (1 - _outputs[i]) * (1 + _outputs[i]); // derivative of tanh is (1-y)(1+y)
                double sum = 0.0;

                for (int j = 0; j < _nextLayerGradients.Length; j++)
                    sum += _nextLayerGradients[j] * _weights[_index++];

                _outputGradients[i] = derivative * sum; // oGrad = (1 - O)(O) * (T-O) - x, y move control - area
            }

            return _outputGradients;
        }

        public UpdateParams UpdateWeights(double[] _weights, int _numNetworks, double[] _prevWeightsDelta, double _learningRate, double _momentum, double[] _gradients, double[] _inputs)
        {
            UpdateParams _up = new UpdateParams();

            int _index = 0;
            int _index2 = 0;
            for (int i = 0; i < _weights.Length; i += _numNetworks) // 0..2 (3)
            {
                for (int j = 0; j < _numNetworks; ++j) // 0..3 (4)
                {
                    double delta = _learningRate * _gradients[j] * _inputs[_index2]; // compute the new delta = "eta * hGrad * input"
                    _weights[_index] += delta; // update
                    _weights[_index] += _momentum * _prevWeightsDelta[_index]; // add mom_momentumentum using previous delta. on first pass old value will be 0.0 but that's OK.
                    _prevWeightsDelta[_index++] = delta; // save the delta for next time
                }

                _index2++;
            }

            _up._weights = _weights;
            _up._delta = _prevWeightsDelta;

            return _up;
        }

        public UpdateParams UpdateBiases(double[] _biases, double[] _prevBiasesDelta, double _learningRate, double _momentum, double[] _gradients)
        {
            UpdateParams _up = new UpdateParams();

            for (int i = 0; i < _biases.Length; ++i)
            {
                double delta = _learningRate * _gradients[i] * 1.0; // the 1.0 is the constant input for any bias; could leave out
                _biases[i] += delta;
                _biases[i] += _momentum * _prevBiasesDelta[i];
                _prevBiasesDelta[i] = delta; // save delta
            }

            _up._weights = _biases;
            _up._delta = _prevBiasesDelta;

            return _up;
        }

        public double Error(double[] tValues, double[] yValues)
        {
            double sum = 0.0;
            for (int i = 0; i < tValues.Length; ++i)
                sum += (tValues[i] - yValues[i]) * (tValues[i] - yValues[i]);
            return Math.Sqrt(sum);
        }
    }
}
