using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_III
{
    class Operation
    {
        public int id_cpt;
        public string type;
        public DateTime date_op;
        public decimal solde_init;
        public int age;
        public int entree;
        public int sortie;
        public string status;

        public Operation(int id, string tp, DateTime d, decimal s, int a, int e, int st, string ss)
        {
            id_cpt = id;
            type = tp;
            date_op = d;
            solde_init = s;
            age = a;
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

                            if (infos.Length >= 7)
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                string type = infos[1];
                                bool estDate = DateTime.TryParse(infos[2], out DateTime date_op);
                                bool estDec = decimal.TryParse(infos[3], out decimal solde_init);
                                bool estAge = int.TryParse(infos[4], out int age);
                                bool estInt2 = int.TryParse(infos[5], out int entree);
                                bool estInt3 = int.TryParse(infos[6], out int sortie);
                                string status = "KO";

                                if (!estInt2)
                                {
                                    entree = -1;
                                }

                                if (!estInt3)
                                {
                                    sortie = -1;
                                }

                                if (!estDec)
                                {
                                    solde_init = -1m;
                                }

                                if (!estAge)
                                {
                                    age = -1;
                                }

                                if (estInt1 && estDate)
                                {
                                    Operation operation_iv = new Operation(id_cpt, type, date_op, solde_init,age, entree, sortie, status);
                                    operations.Add(operation_iv);
                                }
                            }
                        }
                    }
                }
            }
            return operations;
        }

        public static Dictionary<int,Compte> VerifyOp(List<Operation> operations, Dictionary<int,Gestionnaire> gestionnaires)
        {
            Dictionary<int, Compte> comptes = new Dictionary<int, Compte>();

            foreach (Operation operation in operations)
            {
                if (operation.entree > 0 && operation.sortie < 0 && operation.solde_init >= 0)   // Creation du compte
                {
                    if ((!comptes.ContainsKey(operation.id_cpt)) && gestionnaires.ContainsKey(operation.entree))   // compte n'existe && gest existe 
                    {
                        if (operation.type == "J" && operation.age >= 8 && operation.age <= 17)   // Compte jeune
                        {
                            operation.status = "OK";
                            CompteJeune compteJeune = new CompteJeune(0, null, new DateTime(), new DateTime(), 0m, new List<Tuple<DateTime, int>>(), new List<Tuple<DateTime, decimal>>(), 0);
                            compteJeune.id_cpt = operation.id_cpt;
                            compteJeune.type = operation.type;
                            compteJeune.date_creation = operation.date_op;
                            compteJeune.solde = operation.solde_init + operation.age * 10m;
                            compteJeune.age = operation.age;
                            compteJeune.id_gs.Add(new Tuple<DateTime, int>(operation.date_op, operation.entree));
                            gestionnaires[operation.entree].comptes.Add(operation.id_cpt);
                            comptes[operation.id_cpt] = compteJeune;
                        }
                        else if (operation.type=="L")         // Livret
                        {
                            operation.status = "OK";
                            CompteLivret compteLivret = new CompteLivret(0, null, new DateTime(), new DateTime(), 0m, new List<Tuple<DateTime, int>>(), new List<Tuple<DateTime, decimal>>(), 0m);
                            compteLivret.id_cpt = operation.id_cpt;
                            compteLivret.type = operation.type;
                            compteLivret.date_creation = operation.date_op;
                            compteLivret.solde = operation.solde_init;
                            compteLivret.id_gs.Add(new Tuple<DateTime, int>(operation.date_op, operation.entree));
                            gestionnaires[operation.entree].comptes.Add(operation.id_cpt);
                            comptes[operation.id_cpt] = compteLivret;
                        }
                        else if (operation.type=="T" && operation.solde_init>=200m)   // CompteATerme
                        {
                            operation.status = "OK";
                            CompteATerme compteATerme = new CompteATerme(0, null, new DateTime(), new DateTime(), 0m, new List<Tuple<DateTime, int>>(), new List<Tuple<DateTime, decimal>>(), 0m, new DateTime());
                            compteATerme.id_cpt = operation.id_cpt;
                            compteATerme.type = operation.type;
                            compteATerme.date_creation = operation.date_op;
                            compteATerme.solde = operation.solde_init;
                            compteATerme.date_fin = operation.date_op.AddYears(5);
                            compteATerme.id_gs.Add(new Tuple<DateTime, int>(operation.date_op, operation.entree));
                            gestionnaires[operation.entree].comptes.Add(operation.id_cpt);
                            comptes[operation.id_cpt] = compteATerme;
                        }
                        else if (operation.type=="C")     // Compte Courant
                        {
                            operation.status = "OK";
                            comptes[operation.id_cpt] = new Compte(0, null, new DateTime(), new DateTime(), 0m, new List<Tuple<DateTime, int>>(), new List<Tuple<DateTime, decimal>>());
                            comptes[operation.id_cpt].id_cpt = operation.id_cpt;
                            comptes[operation.id_cpt].type = operation.type;
                            comptes[operation.id_cpt].date_creation = operation.date_op;
                            comptes[operation.id_cpt].solde = operation.solde_init;
                            comptes[operation.id_cpt].id_gs.Add(new Tuple<DateTime, int>(operation.date_op, operation.entree));
                            gestionnaires[operation.entree].comptes.Add(operation.id_cpt);
                        }
;
                    }
                }
                if (operation.entree < 0 && operation.sortie > 0)     // Résiliation du compte
                {
                    if (comptes.ContainsKey(operation.id_cpt) && gestionnaires[operation.sortie].comptes.Contains(operation.id_cpt)) // compte existe && compte appartient au gest correct
                    {
                        
                        if (comptes[operation.id_cpt].type=="T")
                        {
                            CompteATerme compteATerme = (CompteATerme)comptes[operation.id_cpt];
                            if (operation.date_op > compteATerme.date_fin)
                            {
                                operation.status = "OK";
                                comptes[operation.id_cpt].date_resili = operation.date_op;
                                gestionnaires[operation.sortie].comptes.Remove(operation.id_cpt);
                            }
                        }
                        else
                        {
                            operation.status = "OK";
                            comptes[operation.id_cpt].date_resili = operation.date_op;
                            gestionnaires[operation.sortie].comptes.Remove(operation.id_cpt);
                        }
                    }

                }
                else if (operation.entree > 0 && operation.sortie > 0)  // Transfer du gestionnaire
                {
                    if (gestionnaires[operation.entree].comptes.Contains(operation.id_cpt) && gestionnaires.ContainsKey(operation.sortie))  // gest original a ce compte && gest nouveau existe
                    {
                        operation.status = "OK";
                        comptes[operation.id_cpt].id_gs.Insert(0, new Tuple<DateTime, int>(operation.date_op, operation.sortie));
                        gestionnaires[operation.sortie].comptes.Add(operation.id_cpt);
                        gestionnaires[operation.entree].comptes.Remove(operation.id_cpt);
                    }
                }
            }
            return comptes;
        }

        public static void WriteOpFile(string outfile, List<string> OpStatus)
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
                        foreach (var status in OpStatus)
                        {
                            srw.WriteLine(status);
                        }
                    }
                }
            }
        }
    }
}
