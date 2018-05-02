using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarxMachineLearning
{
    public struct TwoArrays
    {
        public double[] _arrayA;
        public double[] _arrayB;
    }

    public class ArrayHelperFunctions
    {
        public ArrayHelperFunctions()
        {
        }

        public double[] ReorderArrayAsInterleave(double[] _input, int _groupSize)
        {
            int _numberOfGroups = _input.Length / _groupSize;
            double[] _reorderedArray = new double[_input.Length];
            int _index = 0;
            int _index2 = 0;
            int _bp = 0;

            for (int j = 0; j < _numberOfGroups; j++)
            {
                for (int i = 0; i < _groupSize; i++)
                {
                    _reorderedArray[_index] = _input[_bp + _index2];
                    _index++;
                    _bp += _numberOfGroups;

                    if (_bp == _input.Length - _numberOfGroups)
                        _bp = 0;
                }
                _index2++;
            }

            return _reorderedArray;
        }

        public double[] MergeArraysSequentially(double[] _firstArray, double[] _secondArray)
        {
            double[] _mergedArray = new double[_firstArray.Length + _secondArray.Length];

            Array.Copy(_firstArray, _mergedArray, _firstArray.Length);
            Array.Copy(_secondArray, 0, _mergedArray, _firstArray.Length, _secondArray.Length);

            return _mergedArray;
        }

        public TwoArrays SeparateMergedArrays(double[] _input, int _splitPoint)
        {
            TwoArrays _ta = new TwoArrays();
            _ta._arrayA = new double[_splitPoint];
            _ta._arrayB = new double[_input.Length - _splitPoint];

            Array.Copy(_input, 0, _ta._arrayA, 0, _splitPoint);
            Array.Copy(_input, _splitPoint, _ta._arrayB, 0, _ta._arrayB.Length);

            return _ta;
        }

        public double[] ReorderArrayByColum(double[] _theArray, int _numInputs, int _numNetworks)
        {
            double[] _reOrderedArray = new double[_theArray.Length];

            int _index = 0;
            int _index2 = 0;

            for (int j = 0; j < _numNetworks; j++)
            {
                _index2 = j;
                for (int i = 0; i < _numInputs; i++)
                {
                    _reOrderedArray[_index++] = _theArray[_index2];
                    _index2 += _numNetworks;
                }
            }

            return _reOrderedArray;
        }
    }
}
