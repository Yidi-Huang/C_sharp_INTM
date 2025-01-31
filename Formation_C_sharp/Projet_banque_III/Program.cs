﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_banque_III
{
    class Program
    {
        static void Main()
        {
            string inputGests = "C:\\Users\\Formation\\Downloads\\Gests3.csv";
            string inputComptes  = "C:\\Users\\Formation\\Downloads\\Compte3.csv";

            string outputOps = "C:\\Users\\Formation\\Downloads\\StatusOp3.csv";

            Dictionary<int, Gestionnaire> gests = Gestionnaire.ChargeGestionnaire(inputGests);
            List<Operation> operations = Operation.ChargeOp(inputComptes);
            Dictionary<int, Compte> comptes = Operation.VerifyOp(operations, gests);

            List<string> StatusOps = new List<string>();

            //--------------------------------------------------------------

            foreach (var op in operations)
            {
                StatusOps.Add($"{op.id_cpt};{op.status}");
            }
            Operation.WriteOpFile(outputOps,StatusOps);

            foreach (var cs in comptes)
            {
                Console.WriteLine(cs.Key+" "+cs.Value+" "+cs.Value.date_resili);
            }

            //:---------------------------------------------------------------


            Console.ReadKey();

        }
    }
}
