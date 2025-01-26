using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public struct PclData
    {
        /// <summary>
        /// Moyenne 
        /// </summary>
        public double Mean { get; set; }
        /// <summary>
        /// Ecart-type
        /// </summary>
        public double StandardDeviation { get; set; }
        /// <summary>
        /// Fraction
        /// </summary>
        public double Fraction { get; set; }
    }

    public class PercolationSimulation
    {
        Random rnd = new Random();

        public PclData MeanPercolationValue(int size, int t)
        {
            double sum = 0;        
            double sumSq = 0;

            for (int i = 0; i < t; i++)
            {
                double p = PercolationValue(size); 
                sum += p;                      
                sumSq += p * p;            
            }

            double m = sum / t;

            double StDv = Math.Sqrt(Math.Abs((sumSq / t) - (m * m)));

            return new PclData
            {
                Mean = m,
                StandardDeviation = StDv,
                Fraction = 0
            };
        }

        public double PercolationValue(int size)
        {

            double cases = size * size;
            Percolation Percolation = new Percolation(size);

            double oc = 0;
            bool percolate = false;

            while (!percolate)
            {
                
                int i = rnd.Next(0, size);
                int j = rnd.Next(0, size);

                while (Percolation.IsOpen(i,j))
                {
                    i = rnd.Next(0, size);
                    j = rnd.Next(0, size);
                }


                Percolation.Open(i, j);
                oc++;

                percolate = Percolation.Percolate();
            }

            double res = oc / cases;

            return res;
        }
    }
}
