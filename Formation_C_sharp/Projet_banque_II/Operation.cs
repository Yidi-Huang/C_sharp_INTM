using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_II
{
    public class Operation
    {
        public int id_cpt;
        public DateTime date_op;
        public decimal solde_init;
        public int entree;
        public int sortie;
        public string status;

        public Operation(int id, DateTime d, decimal s, int e, int st,string ss)
        {
            id_cpt = id;
            date_op = d;
            solde_init = s;
            entree = e;
            sortie = st;
            status = ss;
        }

        public static List<Operation> ChargeOp(string input)
        {
            List<Operation> operations = new List<Operation>();
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
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                bool estDate = DateTime.TryParse(infos[1], out DateTime date_op);
                                bool estDec = decimal.TryParse(infos[2], out decimal solde_init);
                                bool estInt2 = int.TryParse(infos[3], out int entree);
                                bool estInt3 = int.TryParse(infos[4], out int sortie);
                                string status = "KO";

                                if (!estInt2)
                                {
                                    entree = -1;
                                }

                                if (!estInt3)
                                {
                                    sortie = -1;
                                }

                                if(!estDec)
                                {
                                    solde_init = -1m;
                                }

                                if (estInt1 && estDate)
                                {
                                    Operation operation_iv = new Operation(id_cpt, date_op, solde_init,entree,sortie,status);
                                    operations.Add(operation_iv);
                                }
                            }
                        }
                    }
                }
            }
            return operations;
        }

        public static void VerifyStatus(List<Operation> operations, Dictionary<int,Gestionnaire> gestionnaires)
        {
            Dictionary<int, Compte> comptes = new Dictionary<int, Compte>();   // commencer : enregistrer les comptes infos

            foreach (Operation operation in operations)
            {
                if (operation.entree>0 && operation.sortie<0)   // Creation du compte
                {
                    if ((!comptes.ContainsKey(operation.id_cpt))&& operation.solde_init>=0 && gestionnaires.ContainsKey(operation.entree))   // compte n'existe && solde positif && gest existe 
                    {
                        operation.status = "OK";

                        comptes[operation.id_cpt] = new Compte(0, new DateTime(),new DateTime(),0,new Dictionary<int, DateTime>(),new Dictionary<DateTime, decimal>());
                        comptes[operation.id_cpt].id_cpt = operation.id_cpt;
                        comptes[operation.id_cpt].solde = operation.solde_init;
                        comptes[operation.id_cpt].date_creation = operation.date_op;
                        comptes[operation.id_cpt].id_gs[operation.entree] = operation.date_op;
                        gestionnaires[operation.entree].comptes.Add(operation.id_cpt);
                    }
                }
                else if (operation.entree<0 && operation.sortie>0)  // Resilisation du compte
                {
                    if(comptes.ContainsKey(operation.id_cpt) && gestionnaires[operation.sortie].comptes.Contains(operation.id_cpt)) // compte existe && compte appartient au gest coorect
                    {
                        operation.status = "OK";
                        comptes[operation.id_cpt].date_resili = operation.date_op;
                        gestionnaires[operation.sortie].comptes.Remove(operation.id_cpt);
                    }
                }
                else if (operation.entree>0 && operation.sortie>0)  // Transfer du gestionnaire
                {
                    if (gestionnaires[operation.entree].comptes.Contains(operation.id_cpt) && gestionnaires.ContainsKey(operation.sortie))  // gest original a ce compte && gest nouveau existe
                    {
                        operation.status = "OK";
                        comptes[operation.id_cpt].id_gs[operation.sortie] = operation.date_op;
                        gestionnaires[operation.sortie].comptes.Add(operation.id_cpt);
                        gestionnaires[operation.entree].comptes.Remove(operation.id_cpt);
                    }
                }
            }
        }
    }

}