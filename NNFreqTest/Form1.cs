using MarxBitcoinLib;
using MarxMachineLearning;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NNFreqTest
{
    public partial class Form1 : Form
    {
        int[] rngTest = new int[256];
        Series series1;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RNGTest1((int)numericUpDown1.Value);
            DrawChart();

        }

        private void RNGTest1(int loopCount)
        {
            rngTest = new int[256];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte buffer = new byte();

            BTCKeyPair kp = BitcoinKey.GenerateKey();
            double[] exampleAddressDbl = new double[kp.PublicAddressTruncated.Length];

            for (int i = 0; i < exampleAddressDbl.Length; i++)
                exampleAddressDbl[i] = 1.0 / (int)kp.PublicAddressTruncated[i];

            for (int i = 0; i < loopCount; i++)
            {
                buffer = ExecuteNetwork(exampleAddressDbl);
                rngTest[(int)(buffer)]++;
            }
        }

        private void DrawChart()
        {
            this.chart1.Series.Clear();
            this.chart1.Annotations.Clear();
            this.chart1.ChartAreas.Clear();
            this.chart1.Legends.Clear();
            this.chart1.Titles.Clear();

            series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Integer",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Bar
            };

            var chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea
            {
            };

            this.chart1.Titles.Add("Frequency Test");

            this.chart1.Series.Add(series1);
            this.chart1.ChartAreas.Add(chartArea1);

            for (int i = 0; i < rngTest.Length; i++)
                series1.Points.Add(rngTest[i]);

            chart1.Invalidate();
        }

        private byte ExecuteNetwork(double[] exampleAddressDbl)
        {
            NeuralNetwork nn = new NeuralNetwork();
            ScalingFunction sf = new ScalingFunction();
            ActivationFunctions af = new ActivationFunctions();

            double[][] networkWeights = new double[5][];
            double[][] networkBias = new double[5][];

            WeightsGenerator wg = new WeightsGenerator();
            int[] networks1 = new int[] { 8, 6, 4, 2, 1 };

            networkWeights[0] = wg.CreateWeights(3, networks1[0] * 32);
            networkWeights[1] = wg.CreateWeights(3, networks1[1] * 8);
            networkWeights[2] = wg.CreateWeights(3, networks1[2] * 6);
            networkWeights[3] = wg.CreateWeights(3, networks1[3] * 4);
            networkWeights[4] = wg.CreateWeights(3, networks1[4] * 2);

            networkBias[0] = Enumerable.Repeat(0.00, networks1[0] * 32).ToArray();
            networkBias[1] = Enumerable.Repeat(0.00, networks1[1] * 8).ToArray();
            networkBias[2] = Enumerable.Repeat(0.00, networks1[2] * 6).ToArray();
            networkBias[3] = Enumerable.Repeat(0.00, networks1[3] * 4).ToArray();
            networkBias[4] = Enumerable.Repeat(0.00, networks1[4] * 2).ToArray();

            double[] layerOneOutput = nn.PerceptronLayer(networks1[0], exampleAddressDbl, networkWeights[0], exampleAddressDbl.Length, networkBias[0]);
            layerOneOutput = af.TanSigmoid(layerOneOutput);

            double[] layerTwoOutput = nn.PerceptronLayer(networks1[1], layerOneOutput, networkWeights[1], layerOneOutput.Length, networkBias[1]);
            layerTwoOutput = af.TanSigmoid(layerTwoOutput);

            double[] layerThreeOutput = nn.PerceptronLayer(networks1[2], layerTwoOutput, networkWeights[2], layerTwoOutput.Length, networkBias[2]);
            layerThreeOutput = af.TanSigmoid(layerThreeOutput);

            double[] layerFourOutput = nn.PerceptronLayer(networks1[3], layerThreeOutput, networkWeights[3], layerThreeOutput.Length, networkBias[3]);
            layerFourOutput = af.TanSigmoid(layerFourOutput);

            double[] layerOutput = nn.PerceptronLayer(networks1[4], layerFourOutput, networkWeights[4], layerFourOutput.Length, networkBias[4]);
            layerOutput = af.TanSigmoid(layerOutput);

            return (byte)(255.0 * layerOutput[0]);
        }
    }
}
