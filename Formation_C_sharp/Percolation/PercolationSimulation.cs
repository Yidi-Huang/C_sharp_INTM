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
        public PclData MeanPercolationValue(int size, int t)
        {
            return new PclData();
        }

        public double PercolationValue(int size)
        {
            double cases = size * size;
            Percolation Percolation = new Percolation(size);

            double oc = 0;
            bool percolate = false;

            
            while(!percolate)
            {
                Random rnd = new Random();  
                int i = rnd.Next(0, size);
                int j = rnd.Next(0, size);

                while (Percolation.IsOpen(i,j))
                {
                    i = rnd.Next(0, size);
                    j = rnd.Next(0, size);
                }

                if (!Percolation.IsOpen(i,j))
                {
                    Percolation.Open(i, j);
                    oc++;
                }
                percolate = Percolation.Percolate();
            }

            double res = oc / cases;

            return res;
        }
    }
}
