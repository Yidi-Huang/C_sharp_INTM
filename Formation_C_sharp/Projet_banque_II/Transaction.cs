using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_II
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
        public static int nb_ok { get; set; }
        public static int nb_ko { get; set; }
        public static decimal sum_ok { get; set; }

        public Transaction(int id, DateTime date, decimal m, int ex, int ds, string st)
        {
            id_trs = id;
            date_trs = date;
            montant = m;
            cpt_ex = ex;
            cpt_ds = ds;
            status = st;
            nb_trs++;
        }

        static Transaction()
        {
            nb_trs = 0;
            nb_ok = 0;
            nb_ko = 0;
            sum_ok = 0m;
        }

        public static Dictionary<int,Transaction> ChargeTransaction(string input)
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
                                    if (! transactions.ContainsKey(id_trs))
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

        public static void ProcessTrans(Dictionary<int, Compte> comptes, Dictionary<int, Transaction> transactions, Dictionary<int, Gestionnaire> gestionnaires)
        {
            DateTime date_org = new DateTime();
            foreach(var pair in transactions)
            {
                Transaction transaction = pair.Value;

                Compte compte_ex = null;
                Compte compte_ds = null;

                Gestionnaire gs_ex = null;
                Gestionnaire gs_ds = null;

                foreach (var id_compte in comptes.Keys)
                {
                    if (id_compte == transaction.cpt_ex)
                    {
                        compte_ex = comptes[id_compte];
                    }
                    else if (id_compte == transaction.cpt_ds)
                    {
                        compte_ds = comptes[id_compte];
                    }
                }


                if (transaction.cpt_ex==0)    // Depot
                {
                    if (comptes.ContainsKey(transaction.cpt_ds) && transaction.montant>0)  
                    {
                        if (transaction.date_trs>compte_ds.date_creation && (transaction.date_trs==date_org || 
                            transaction.date_trs<compte_ds.date_resili))
                        {
                            transaction.status = "OK";
                            compte_ds.solde += transaction.montant;
                        }
                    }
                }

                if (transaction.cpt_ds == 0)   // Retrait
                {
                    if(comptes.ContainsKey(transaction.cpt_ex) && transaction.montant>0 && compte_ex.solde>=transaction.montant)
                    {
                        // Verifier his_soldes : 1. nb_trs <=1000 ; 2. dans 7 jours <=2000
                    }
                }
            }
        }

        public static int FindCurentGest(DateTime date_trs, List<Tuple<DateTime, int>> id_gs)
        {
            int id = 0;
            


            return id;
        }
    }
}
