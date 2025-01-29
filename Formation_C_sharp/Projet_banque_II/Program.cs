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
            string outputOps = "C:\\Users\\Formation\\Downloads\\StatutOpe.csv";
            string inputTrs = "C:\\Users\\Formation\\Downloads\\Transaction2.csv";

            Dictionary<int, Gestionnaire> gests = Gestionnaire.ChargeGestionnaire(inputGests);
            List<Operation> operations = Operation.ChargeOp(inputComptes);
            List<string> opstatus = new List<string>();
            Dictionary<int, Compte> comptes = Operation.VerifyStatus(operations, gests);

            foreach (var p in operations)
            {
                opstatus.Add($"{p.id_cpt};{p.status}");
            }

            Operation.WriteOpFile(outputOps, opstatus);


            Dictionary<int, Transaction> transactions = Transaction.ChargeTransaction(inputTrs);

            foreach (var p in comptes)
            {
                Compte compte = p.Value;
                foreach(var h in compte.id_gs)
                {
                    Console.WriteLine(h.Item1+" "+h.Item2);
                }
            }

            Transaction.ProcessTrans(comptes, transactions, gests);
            foreach (var pp in transactions)
            {
                Console.WriteLine(pp.Value.id_trs+" "+ pp.Value.date_trs+" "
                    +pp.Value.montant+" "+pp.Value.cpt_ex+" "+pp.Value.cpt_ds+" "+pp.Value.status);
            }

            Console.ReadKey();
        }
    }
}
