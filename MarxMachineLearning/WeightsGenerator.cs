using System;
using System.Security.Cryptography;

namespace MarxMachineLearning
{
    public class WeightsGenerator
    {
        Random _rnd = new Random((int)DateTime.Now.Ticks);
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        /*
         * The behaviour of C# Random() can change depending on the power source
         * on some laptops.  Reason is unknown as of yet.
         * 
         * RndXorshift is used as a fast replacement in the method 
         * CreateRandomWeightsPositiveAndNegative().
         */
        //RndXorshift rng = new RndXorshift();

        public WeightsGenerator()
        {
        }

        public double[] CreateWeights(int _type, int _amount)
        {
            switch (_type)
            {
                case 0:
                    return CreateRandomWeightsPositive(_amount);
                case 1:
                    return CreateRandomWeightsNegative(_amount);
                case 3:
                    return CreateRandomWeightsPositiveAndNegative(_amount);
                default:
                    return CreateRandomWeightsPositiveAndNegative(_amount);
            }
        }

        public double[] CreateRandomWeightsPositive(int _numElements)
        {
            //Random _rnd = new Random((int)DateTime.Now.Ticks);

            double[] _weights = new double[_numElements];
            

            for (int i = 0; i < _numElements; i++)
                _weights[i] = _rnd.NextDouble();

            return _weights;
        }

        public double[] CreateRandomWeightsNegative(int _numElements)
        {
            //Random _rnd = new Random((int)DateTime.Now.Ticks);

            double[] _weights = new double[_numElements];

            for (int i = 0; i < _numElements; i++)
                _weights[i] = _rnd.NextDouble();

            return _weights;
        }

        public double[] CreateRandomWeightsPositiveAndNegative(int _numElements)
        {
            //Random _rnd = new Random((int)DateTime.Now.Ticks);

            double[] _weights = new double[_numElements];
            var bytes = new Byte[8];

            for (int i = 0; i < _numElements; i++)
            {
                if (_rnd.Next(1, 3) == 1)
                {
                    rng.GetBytes(bytes);
                    var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                    _weights[i] = ul / (Double)(1UL << 53);
                }
                else
                {
                    rng.GetBytes(bytes);
                    var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
                    _weights[i] = -(ul / (Double)(1UL << 53));
                }
            }

            return _weights;
        }
    }
}
