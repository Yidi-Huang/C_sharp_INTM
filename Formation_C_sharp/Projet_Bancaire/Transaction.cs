using System;
using System.Collections.Generic;
using System.IO;

namespace Projet_Bancaire
{
    public class Transaction
    {
        public int id_trs;
        public decimal solde_trs;
        public int cpt_ex;
        public int cpt_ds;

        public Transaction(int ID_C, decimal SOLDE, int ID_EX, int ID_DS)
        {
            id_trs = ID_C;
            solde_trs = SOLDE;
            cpt_ex = ID_EX;
            cpt_ds = ID_DS;
        }
        public static List<Transaction> ChargeTrans(string input)
        {
            List<Transaction> transactions = new List<Transaction>();

            if (File.Exists(input))
            {
                using (FileStream file = File.Open(input, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            string ligne = sr.ReadLine();
                            string[] infos = ligne.Split(';');

                            if (infos.Length == 4)
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_trs);
                                bool estDec = decimal.TryParse(infos[1], out decimal solde_trs);
                                bool estInt2 = int.TryParse(infos[2], out int cpt_ex);
                                bool estInt3 = int.TryParse(infos[3], out int cpt_ds);

                                if (estInt1 && estDec && estInt2 && estInt3)
                                {
                                    Transaction transaction = new Transaction(id_trs, solde_trs, cpt_ex, cpt_ds);
                                    transactions.Add(transaction);
                                }
                                else
                                {
                                    Console.WriteLine($"Invalid data: {ligne}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Malformed line: {ligne}");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"File not found: {input}");
            }

            return transactions;
        }
    }
}
