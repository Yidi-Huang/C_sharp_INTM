using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_banque_II
{
    class Program
    {
        static void Main()
        {
            string inputComptes = "C:\\Users\\Formation\\Downloads\\Compte2.csv";
            string inputGests = "C:\\Users\\Formation\\Downloads\\Gests.csv";
            string outputFile = "C:\\Users\\Formation\\Downloads\\StatutsTransactions.csv";

            Dictionary<int, Gestionnaire> gests = Gestionnaire.ChargeGestionnaire(inputGests);
            foreach (var pair in gests)
            {
                Console.WriteLine(pair.Key.ToString()+" "+ pair.Value.nb_trs);
            }

            Console.WriteLine(" ");

            List<Operation> operations = Operation.ChargeOp(inputComptes);
            Operation.VerifyStatus(operations, gests);
            foreach (var p in operations)
            {
                Console.WriteLine(p.id_cpt+" "+p.date_op+" "+p.solde_init+" "+p.entree+" "+p.sortie+" "+p.status);
            }

            DateTime h=new DateTime();
            Console.WriteLine(h);

            Console.ReadKey();
        }
    }
}
