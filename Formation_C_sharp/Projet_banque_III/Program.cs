using System;
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

            Dictionary<int, Gestionnaire> gests = Gestionnaire.ChargeGestionnaire(inputGests);
            List<Operation> operations = Operation.ChargeOp(inputComptes);
            Dictionary<int, Compte> comptes = Operation.VerifyOp(operations, gests);

            //--------------------------------------------------------------
            foreach (var gs in gests)
            {
                Gestionnaire gest = gs.Value;
                Console.WriteLine(gest.id_gs + " " + " " + gest.type_gs + " " + gest.nb_trs);
            }
            Console.WriteLine("");

            foreach (var ops in operations)
            {
                Console.WriteLine(ops.id_cpt + " " + ops.status);
            }
            Console.WriteLine("");

            foreach (var cs in comptes)
            {
                Compte c = cs.Value;
                Console.WriteLine(c.id_cpt+" "+c.age+" "+c.solde);
            }
            //:---------------------------------------------------------------


            Console.ReadKey();

        }
    }
}
