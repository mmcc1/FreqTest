using System;
using System.Collections.Generic;

namespace MarxMachineLearning
{
    public class GeneticAlgorithm
    {
        Random rng = new Random((int)DateTime.Now.Ticks);
        double[] oldweights;

        public GeneticAlgorithm()
        {
        }

        public double[] GeneratePopulation(int _amount, int _type)
        {
            WeightsGenerator _wg = new WeightsGenerator();

            return _wg.CreateWeights(_type, _amount);
        }

        //Check for all true and no solution - if so, start entire algorithm again from the beginning.
        //_minFactor and _maxFactor are arbitrary, but -0.2 and 0.2 is a good start (Genetic compatibility??? Same species???)
        public bool[] EvaluateFitness(double[] _parent1Weights, double[] _parent2Weights, double _minFactor, double _maxFactor)
        {
            bool[] _shouldKeep = new bool[_parent1Weights.Length];

            for (int i = 0; i < _parent1Weights.Length; i++)
            {
                if (_parent1Weights[i] - _parent2Weights[i] < _maxFactor && _parent1Weights[i] - _parent2Weights[i] > _minFactor)
                    _shouldKeep[i] = true;
                else
                    _shouldKeep[i] = false;
            }

            return _shouldKeep;
        }

        public double[] CrossOverAndMutation(bool[] _evaluateFitnessResult, double[] _weights, int _type)
        {
            WeightsGenerator _wg = new WeightsGenerator();

            double[] _childWeights = new double[_weights.Length];

            for (int i = 0; i < _weights.Length; i++)
            {
                if (_evaluateFitnessResult[i])
                    _childWeights[i] = _weights[i];
                else
                {
                    _childWeights[i] = _wg.CreateWeights(_type, 1)[0];
                }
            }

            return _childWeights;
        }

        public double[] CrossOverAndMutation(double[] _weights, int _type, bool closer)
        {
            WeightsGenerator _wg = new WeightsGenerator();

            if (closer)
            {
                //oldweights = _weights;

                //Select some weights at random and record
                List<int> weightIndexes = new List<int>();

                for (int i = 0; i < _weights.Length / 5; i++)
                    weightIndexes.Add(rng.Next(_weights.Length));

                //shift values up/down by around 2%
                for (int i = 0; i < weightIndexes.Count; i++)
                    _weights[weightIndexes[i]] = _weights[weightIndexes[i]] + rng.NextDouble();

                weightIndexes.Clear();
            }
            else
                _weights = _wg.CreateWeights(3, _weights.Length);

            //on next pass check if closer, if not go the other way, if so, repeat.

            return _weights;
        }
    }
}
