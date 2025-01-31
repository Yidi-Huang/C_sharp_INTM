using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_III
{
    public class Transaction
    {
        public int id_trs { get; }
        public DateTime date_trs { get; }
        public decimal montant { get; }
        public int cpt_ex { get; }
        public int cpt_ds { get; }
        public string status { get; set; }
        public static int nb_trs { get; set; }

        public Transaction(int id, DateTime dt, decimal m, int ex, int ds, string st)
        {
            id_trs = id;
            date_trs = dt;
            montant = m;
            cpt_ex = ex;
            cpt_ds = ds;
            status = st;
            nb_trs++;
        }

        public static Dictionary<int, Transaction> ChargeTransaction(string input)
        {
            Dictionary<int, Transaction> transactions = new Dictionary<int, Transaction>();
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

                            if (infos.Length >= 5)
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_trs);
                                bool estDate = DateTime.TryParse(infos[1], out DateTime date_trs);
                                bool estDec = decimal.TryParse(infos[2], out decimal montant);
                                bool estInt2 = int.TryParse(infos[3], out int cpt_ex);
                                bool estInt3 = int.TryParse(infos[4], out int cpt_ds);
                                string status = "KO";

                                if (estInt1 && estDate && estDec && estInt2 && estInt3)
                                {
                                    Transaction transaction_iv = new Transaction(id_trs, date_trs, montant, cpt_ex, cpt_ds, status);
                                    if (!transactions.ContainsKey(id_trs))
                                    {
                                        transactions[id_trs] = transaction_iv;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return transactions;
        }
    }
}
