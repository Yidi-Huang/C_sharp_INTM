using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exercice_C_sharp
{
    class Exercice31
    {
        public static void SchoolMeans(string input, string output)
        {
            FileStream file = null;
            StreamReader sr = null;
            StreamWriter srw = null;
            Dictionary<string, List<double>> dico_note = new Dictionary<string, List<double>>(); // matiere,nb student,sum note

            if (File.Exists(input))
            {
                using (file = File.Open(input, FileMode.Open, FileAccess.Read))
                {
                    if (file != null)
                    {
                        using (sr= new StreamReader(file))
                        {

                            while (!sr.EndOfStream)
                            {
                                sr.ReadLine();
                                string note_l = sr.ReadLine();
                                string[] infos = note_l.Split(';');

                                string matiere = infos[1];
                                double note = double.Parse(infos[2]);

                                if (dico_note.ContainsKey(matiere))
                                {
                                    List<double> ligne = dico_note[matiere];
                                    ligne[0]++;
                                    ligne[1] += note;
                                }
                                else
                                {
                                    List<double> intern = new List<double> { 1, note };
                                    dico_note[matiere] = intern;
                                }

                            }
                            //Console.WriteLine(dico_note["MATHS"][0]);
                        }
                    }
                }
            }
            
            using (file=File.Open(output,FileMode.Create,FileAccess.Write))
            {
                if (file != null)
                {
                    using (srw = new StreamWriter(file))
                    {
                        srw.WriteLine("Matière;Moyenne");
                        foreach (var pair in dico_note)
                        {
                            double moy = pair.Value[1] / pair.Value[0];
                            srw.WriteLine($"{pair.Key};{moy:#0.0}");
                        }
                        
                    }
                }
            }
        }
    }
}
