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
            string inputTrs = "C:\\Users\\Formation\\Downloads\\Transaction2.csv";

            string outputOps = "C:\\Users\\Formation\\Downloads\\StatutOpe.csv";
            string outputTrs = "C:\\Users\\Formation\\Downloads\\StatutTrs.csv";

            Dictionary<int, Gestionnaire> gests = Gestionnaire.ChargeGestionnaire(inputGests);
            List<Operation> operations = Operation.ChargeOp(inputComptes);
            List<string> opstatus = new List<string>();
            List<string> trsstatus = new List<string>();
            Dictionary<int, Compte> comptes = Operation.VerifyStatus(operations, gests);

            foreach (var p in operations)
            {
                opstatus.Add($"{p.id_cpt};{p.status}");
            }

            Operation.WriteOpFile(outputOps, opstatus);


            Dictionary<int, Transaction> transactions = Transaction.ChargeTransaction(inputTrs);

            Transaction.ProcessTrans(comptes, transactions, gests);
            foreach (var t in transactions)
            {
                trsstatus.Add($"{t.Value.id_trs};{t.Value.status}");
                Console.WriteLine(t.Value.id_trs+" "+ t.Value.date_trs+" "
                    +t.Value.montant+" "+t.Value.cpt_ex+" "+t.Value.cpt_ds+" "+t.Value.status);
            }
            Transaction.WriteTrsFile(outputTrs, trsstatus);

            Console.WriteLine(Compte.nb_comptes+" "+Transaction.nb_trs+" "+Transaction.nb_ok+" "+Transaction.nb_ko+" "+Transaction.sum_ok);
            Console.WriteLine(" ");

            foreach (var pair in gests)
            {
                Gestionnaire gest = pair.Value;
                Console.WriteLine(gest.id_gs+" "+gest.frais_gs+" "+gest.type_gs);
            }

            Console.ReadKey();
        }
    }
}
