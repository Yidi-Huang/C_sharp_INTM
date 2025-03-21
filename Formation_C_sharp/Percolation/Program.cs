﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Percolation
{
    class Program
    {
        static void Main()
        {
            // Réponse question : 
            // 1. Pire cas : O(N^^2) on ouvre à la fin la case qui permet la percolation.
            // 2. Parce que on ouvre aléatoirement, on rencontre rarement ce cas : 1/N(N-1). 
            int sz = 4;
            Percolation Percolation = new Percolation(sz);

            int i = 1;
            int j = 1;
            var voisons = Percolation.CloseNeighbors(i, j);
            foreach(var p in voisons)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("");

            PercolationSimulation PercolationSimulation = new PercolationSimulation();
            double v= PercolationSimulation.PercolationValue(sz);
            Console.WriteLine(v);
            double v2 = PercolationSimulation.PercolationValue(sz);
            Console.WriteLine(v2);

            int t = 5;
            var pcldata = PercolationSimulation.MeanPercolationValue(sz, t);
            //Console.WriteLine("");
            Console.WriteLine(pcldata.Mean);
            //Console.WriteLine(pcldata.StandardDeviation);

            Console.WriteLine("fin");

            Console.ReadKey();
        }
    }
}
