using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bancaire
{
    class Program
    {
        static void Main()
        {
            //Console.WriteLine("hi");
            string input = "C:\\Users\\Formation\\Downloads\\Compte.csv";
            List<Compte> cpts = Compte.ChargeCompte(input);
            foreach(Compte c in cpts)
            {
                Console.WriteLine(c.id_cpt + " "+ c.solde + " " + c.his_soldes.Count);
            }


            Console.WriteLine("");

            List<int> banques = Banque.ChargeBanque(input);                  // liste des N comptes
            foreach (var c in banques)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("");


            string input2 = "C:\\Users\\Formation\\Downloads\\Transaction.csv";
            List<Transaction> trs = Transaction.ChargeTrans(input2);                // liste des transactions
            foreach (Transaction t in trs)
            {
                Console.WriteLine(t.id_trs + " " + t.solde_trs + " " + t.cpt_ex + " " + t.cpt_ds);
            }


            Console.ReadKey();

        }

    }
}
