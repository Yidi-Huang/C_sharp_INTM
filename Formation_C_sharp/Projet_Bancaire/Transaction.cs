﻿using System;
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
                            }
                        }
                    }
                }
            }
            return transactions;
        }

        public string ProcessTrans(Transaction transaction, List<Compte> comptes, Dictionary<int,List<Compte>> banques)
        {
            Compte compte_ex = null;
            Compte compte_ds = null;
            string status = "KO";


            foreach (var compte in comptes)
            {
                if (compte.id_cpt == transaction.cpt_ex)
                {
                    compte_ex = compte;
                }
                else if (compte.id_cpt == transaction.cpt_ds)
                {
                    compte_ds = compte;
                }
            }

            if (transaction.cpt_ex == 0) // Depot
            {
                if (Banque.verify_cpt_exist(banques, transaction.cpt_ds) && transaction.solde_trs >= 0)
                {
                    compte_ds.AjoutSolde(transaction.solde_trs);
                    status = "OK";
                }
            }
            else if (transaction.cpt_ds == 0) // Retrait
            {
                if (Banque.verify_cpt_exist(banques, transaction.cpt_ex) && compte_ex.solde >= transaction.solde_trs)
                {
                    decimal sumHis = 0m;
                    if (compte_ex.his_soldes.Count >9)
                    {
                        compte_ex.his_soldes.RemoveAt(0);
                    }
                    foreach (decimal hissolde in compte_ex.his_soldes)
                    {
                        sumHis += hissolde;
                    }
                    sumHis += transaction.solde_trs;

                    if (sumHis<=1000)
                    {
                        compte_ex.RetraitSolde(transaction.solde_trs);
                        status = "OK";
                        compte_ex.his_soldes.Add(transaction.solde_trs);
                    }

                }
            }
            else  // Transfer
            {
                if (Banque.verify_cpt_exist(banques, transaction.cpt_ds) && Banque.verify_cpt_exist(banques, transaction.cpt_ex))
                {
                    if (transaction.solde_trs >= 0 && compte_ex.solde >= transaction.solde_trs)
                    {
                        decimal sumHis = 0m;
                        if (compte_ex.his_soldes.Count > 9)
                        {
                            compte_ex.his_soldes.RemoveAt(0);
                        }
                        foreach (decimal hissolde in compte_ex.his_soldes)
                        {
                            sumHis += hissolde;
                        }
                        sumHis += transaction.solde_trs;

                        if (sumHis <= 1000)
                        {
                            compte_ex.RetraitSolde(transaction.solde_trs);
                            compte_ds.AjoutSolde(transaction.solde_trs);
                            status = "OK";
                            compte_ex.his_soldes.Add(transaction.solde_trs);
                        }


                    }
                }
            }
            return status;

        }

        public static void WriteStatusFile(string outfile, List<string> transactionStatuses)
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
                        foreach (var status in transactionStatuses)
                        {
                            srw.WriteLine(status);
                        }
                    }
                }
            }
        }

    }
}
