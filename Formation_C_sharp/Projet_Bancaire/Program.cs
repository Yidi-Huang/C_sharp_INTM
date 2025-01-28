using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Projet_Bancaire
{
    class Program
    {
        static void Main()
        {
            string inputComptes = "C:\\Users\\Formation\\Downloads\\Compte.csv";
            string inputTransactions = "C:\\Users\\Formation\\Downloads\\Transaction.csv";
            string outputFile = "C:\\Users\\Formation\\Downloads\\StatutsTransactions.csv";


            List<Compte> comptes = Compte.ChargeCompte(inputComptes);
            List<Transaction> transactions = Transaction.ChargeTrans(inputTransactions);
            Dictionary<int, Compte> banques = Banque.ChargeBanque(inputComptes);

            List<string> transactionStatuses = new List<string>();
            Transaction.ProcessTrans(transactions, comptes, banques);

            foreach (Transaction transaction in transactions)
            {
                transactionStatuses.Add($"{transaction.id_trs};{transaction.status}");
                Console.WriteLine($"{transaction.id_trs};{transaction.status}");
            }

            Transaction.WriteStatusFile(outputFile, transactionStatuses);

            Console.WriteLine(" ");
            Console.WriteLine("Soldes des comptes:");
            foreach (var compte in banques.Values)
            {
                Console.WriteLine($"ID: {compte.id_cpt} Solde: {compte.solde}");
            }

            Console.ReadKey();
        }
    }
}
