using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FreqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] rngTest = new int[256];
            int count = 0;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[1];

            while (true)
            {
                //byte[] buffer = new byte[1];
                rng.GetBytes(buffer);
                rngTest[(int)(buffer[0])]++;

                if (count > 50000)
                {
                    Console.Clear();

                    for (int i = 0; i < rngTest.Length; i++)
                    {
                        Console.WriteLine(i + "  :  " + rngTest[i]);
                    }

                    count = 0;
                }
                else
                    count++;
            }
        }
    }
}
