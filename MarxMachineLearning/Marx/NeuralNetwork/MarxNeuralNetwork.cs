using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarxMachineLearning.Marx.NeuralNetwork
{
    public class MarxNeuralNetwork
    {
        public struct Layout
        {
            public int _network;    //x
            public int _layer;      //y
        }

        public struct NeuroTransmitter
        {
            public Guid _bioGUID;
            public double _Activation;
            public List<List<double>> _inputHistory;
            public List<List<double>> _weightsHistory;
            public List<Layout> _layoutHistory;
        }

        public MarxNeuralNetwork()
        {
        }

        public NeuroTransmitter MarxPerceptronCreateNeuroTransmitter(double _input)
        {
            NeuroTransmitter _b = new NeuroTransmitter();

            _b._bioGUID = Guid.NewGuid();

            _b._Activation = _input;

            _b._inputHistory = new List<List<double>>();
            List<double> _currentInput = new List<double>();
            _currentInput.Add(_input);
            _b._inputHistory.Add(_currentInput);

            _b._weightsHistory = new List<List<double>>();
            List<double> _currentWeights = new List<double>();
            _currentWeights.Add(0);
            _b._weightsHistory.Add(_currentWeights);

            _b._layoutHistory = new List<Layout>();
            Layout _currentLayout = new Layout();
            _currentLayout._network = 0;
            _currentLayout._layer = 0;

            return _b;
        }

        public NeuroTransmitter MarxPerceptron(NeuroTransmitter[] _input, double[] _weights, Layout _currentLayout)
        {
            NeuroTransmitter _b = new NeuroTransmitter();

            _b._bioGUID = Guid.NewGuid();
            _b._inputHistory = new List<List<double>>();
            _b._weightsHistory = new List<List<double>>();
            _b._layoutHistory = new List<Layout>();

            List<double> _currentInput = new List<double>();
            List<double> _currentWeights = new List<double>();

            for (int i = 0; i < _input.Length; i++)
            {
                _b._Activation += _input[i]._Activation * _weights[i];               
                _currentInput.Add(_input[i]._Activation);
                _currentWeights.Add(_weights[i]);
            }

            _b._inputHistory.Add(_currentInput);
            _b._weightsHistory.Add(_currentWeights);
            _b._layoutHistory.Add(_currentLayout);

            return _b;
        }
    }
}
