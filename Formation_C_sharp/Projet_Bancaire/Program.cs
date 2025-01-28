using System;
using System.Collections.Generic;

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
            Dictionary<int, List<Compte>> banques = Banque.ChargeBanque(inputComptes);

            List<string> transactionStatuses = new List<string>();
            foreach (Transaction transaction in transactions)
            {
                string status = transaction.ProcessTrans(transaction, comptes, banques);

                transactionStatuses.Add($"{transaction.id_trs};{status}");
                Console.WriteLine($"{transaction.id_trs};{status}");
            }

            Transaction.WriteStatusFile(outputFile, transactionStatuses);

            Console.WriteLine(" ");
            Console.WriteLine("Soldes comptes:");
            foreach (var compte in comptes)
            {
                Console.WriteLine($"ID: {compte.id_cpt} Solde: {compte.solde}");
            }

            Console.ReadKey();
        }
    }
}
