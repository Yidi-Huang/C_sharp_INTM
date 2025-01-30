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

                if (compte_ex != null)
                {
                    //Console.WriteLine(compte_ex.id_gs[0].Item1.ToString());
                    int id_currentgs = FindCurentGest(transaction.date_trs, compte_ex.id_gs);   // trouver id gestionnaire
                    foreach (var id_gest in gestionnaires.Keys)
                    {
                        if (id_gest == id_currentgs)
                        {
                            gs_ex = gestionnaires[id_currentgs];
                        }
                    }
                }

                if (compte_ds != null)
                {
                    int id_currentgs = FindCurentGest(transaction.date_trs, compte_ds.id_gs);   // trouver id gestionnaire
                    foreach (var id_gest in gestionnaires.Keys)
                    {
                        if (id_gest == id_currentgs)
                        {
                            gs_ds = gestionnaires[id_currentgs];
                        }
                    }
                }

                if (transaction.cpt_ex==0)    // Depot
                {
                    if (comptes.ContainsKey(transaction.cpt_ds) && transaction.montant>0)  
                    {
                        if (transaction.date_trs>compte_ds.date_creation && (compte_ds.date_resili == date_org || 
                            transaction.date_trs<compte_ds.date_resili))
                        {
                            transaction.status = "OK";
                            compte_ds.solde += transaction.montant;
                            Transaction.nb_ok++;
                            Transaction.sum_ok += transaction.montant;
                        }
                        else
                        {
                            Transaction.nb_ko++;
                        }
                    }
                    else
                    {
                        Transaction.nb_ko++;
                    }
                }

                if (transaction.cpt_ds == 0)   // Retrait
                {
                    if(comptes.ContainsKey(transaction.cpt_ex) && transaction.montant>0 && compte_ex.solde>=transaction.montant && 
                        transaction.date_trs > compte_ex.date_creation && (compte_ex.date_resili == date_org ||
                        transaction.date_trs < compte_ex.date_resili))
                    {
                        int nb_trs = gs_ex.nb_trs;

                        decimal sum_trs = GetTrsSum(nb_trs, compte_ex.his_soldes);
                        sum_trs += transaction.montant;

                        decimal sum_week = GetWeekSum(transaction.date_trs, compte_ex.his_soldes);
                        sum_week += transaction.montant;

                        if (sum_trs<=1000 && sum_week<=2000)
                        {
                            transaction.status = "OK";
                            compte_ex.solde -= transaction.montant;
                            compte_ex.his_soldes.Add(new Tuple<DateTime, decimal>(transaction.date_trs, transaction.montant));
                            Transaction.nb_ok++;
                            Transaction.sum_ok += transaction.montant;
                        }
                        else
                        {
                            Transaction.nb_ko++;
                        }

                    }
                    else
                    {
                        Transaction.nb_ko++;
                    }
                }
                else if (transaction.cpt_ds !=0 && transaction.cpt_ex !=0)   // Transfer
                {
                    if (comptes.ContainsKey(transaction.cpt_ex) && comptes.ContainsKey(transaction.cpt_ds) && transaction.montant > 0 && compte_ex.solde >= transaction.montant &&
                        transaction.date_trs > compte_ex.date_creation && (compte_ex.date_resili == date_org || transaction.date_trs < compte_ex.date_resili) &&
                        transaction.date_trs > compte_ds.date_creation && (compte_ds.date_resili == date_org || transaction.date_trs < compte_ds.date_resili))
                    {
                        int nb_trs = gs_ex.nb_trs;
                        decimal sum_trs = GetTrsSum(nb_trs, compte_ex.his_soldes);
                        sum_trs += transaction.montant;

                        decimal sum_week = GetWeekSum(transaction.date_trs, compte_ex.his_soldes);
                        sum_week += transaction.montant;

                        if (sum_trs <= 1000 && sum_week <= 2000)
                        {
                            transaction.status = "OK";
                            compte_ex.solde -= transaction.montant;
                            compte_ds.solde += transaction.montant;
                            compte_ex.his_soldes.Add(new Tuple<DateTime, decimal>(transaction.date_trs, transaction.montant));
                            Transaction.nb_ok++;
                            Transaction.sum_ok += transaction.montant;

                            if (gs_ex.id_gs != gs_ds.id_gs)   // deux gests : faut frais de gestion
                            {
                                if (gs_ex.type_gs == "Particulier")
                                {
                                    gs_ex.frais_gs += 0.01m * transaction.montant;
                                }
                                else if (gs_ex.type_gs == "Entreprise")
                                {
                                    gs_ex.frais_gs += 10m;
                                }
                            }
                        }
                        else
                        {
                            Transaction.nb_ko++;
                        }
                    }
                    else
                    {
                        Transaction.nb_ko++;
                    }
                }
            }
        }

        public static int FindCurentGest(DateTime date_trs, List<Tuple<DateTime, int>> id_gs)
        {
            int id =0 ;
            foreach (var tup in id_gs)
            {
                if (date_trs>tup.Item1)
                {
                    id = tup.Item2;
                    break;
                }
            }
            return id;
        }

        public static decimal GetTrsSum(int nb_trs, List<Tuple<DateTime, decimal>> his_soldes)
        {
            decimal sum_trs = 0m;
            if (his_soldes.Count==0)
            {
                sum_trs = 0;
            }
            else if (his_soldes.Count < nb_trs)
            {
                foreach(var tup in his_soldes)
                {
                    decimal s = tup.Item2;
                    sum_trs += s;
                }
            }
            else
            {
                for (int i=his_soldes.Count-nb_trs+1;i<=his_soldes.Count-1;i++)
                {
                    decimal s = his_soldes[i].Item2;
                    sum_trs += s;
                }
            }

            return sum_trs;
        }

        public static decimal GetWeekSum(DateTime date_trs, List<Tuple<DateTime, decimal>> his_soldes)
        {
            decimal sum_week = 0m;
            DateTime start_date = date_trs.AddDays(-6);

            foreach (var tup in his_soldes)
            {
                if (tup.Item1>=start_date)
                {
                    sum_week += tup.Item2;
                }
            }

            return sum_week;
        }

        public static void WriteTrsFile(string outfile, List<string> TrsStatus)
        {
            FileStream file = null;
            StreamWriter srw = null;

            using (file = File.Open(outfile, FileMode.Create, FileAccess.Write))
            {
                if (file != null)
                {
                    using (srw = new StreamWriter(file))
                    {
                        //srw.WriteLine("Matière;Moyenne");
                        foreach (var status in TrsStatus)
                        {
                            srw.WriteLine(status);
                        }
                    }
                }
            }
        }
    }
}
