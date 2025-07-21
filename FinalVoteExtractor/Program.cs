using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinalVoteExtractor {
    class MainClass {
        public enum Esame {
            Analisi1,
            AlgebraLineare,
            Fondamenti,
            Programmazione1,
            Programmazione2,
            Algoritmi,
            Count
        }

        public static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Error. Usage: ./programma <InputFilename> <outputFilename> <ExameTypeNumber>");

                Console.WriteLine("The ExameType are: ");
                for (int i = 0; i < (int)Esame.Count; i++) {
                    Console.WriteLine($"{i}: {Enum.GetName(typeof(Esame), i)}");
                }

                throw new Exception("Wrong number of parameters");
            }

            string inputFilename = args[0];
            string outputFilename = args[1];
            Esame tipoEsame;

            if (int.TryParse(args[2], out int num) && Enum.IsDefined(typeof(Esame), num)) {
                    tipoEsame = (Esame)num;
            } else {
                throw new Exception("Wrong input enum. Call the program without parameters to see the enum list");
            }


            if (!File.Exists(args[0])) {
                throw new Exception("Wrong input file path");
            }

            Dictionary<int, int> mat_voto = new Dictionary<int, int>();


            string line = "";
            using (StreamReader sr = new StreamReader(inputFilename)) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);
                    int voto = GetVoto(fields[1], tipoEsame);

                    /*usando mat_voto[mat] = voto e non la mat_voto.Add(mat, voto) assicuro che se la matricola gia' esiste il valore venga sovrascritto col nuovo
                    Con la .Add genererebbe eccezione*/

                    mat_voto[mat] = voto;
                }
            }

            //Console.WriteLine(mat_voto[928561]);


            using (StreamWriter sw = new StreamWriter(outputFilename)) {
                foreach (KeyValuePair<int, int> kvp in mat_voto) {
                    sw.WriteLine($"{kvp.Key};{kvp.Value}");
                }
            }


        }

        public static int GetVoto(string input, Esame tipoEsame) {
            int voto = 0;

            switch(tipoEsame) {
                case Esame.Analisi1:
                    //esempio di match: "Esame superato con 25 su 30"
                    Regex rg = new Regex(@".+\s(?<votoInTrentesimi>\d+).+\s(?<suTrenta>\d)");
                    Match match = rg.Match(input);

                    //il controllo e' perche' ci puo' anche essere scritto "Assente", "Ritirato", "Filtri non passati"...
                    if (rg.IsMatch(input)) {
                        voto = int.Parse(match.Groups["votoInTrentesimi"].Value);
                    }

                    break;
                case Esame.AlgebraLineare:
                    if (input.Equals("30L")) {
                        voto = 31;
                    } else {
                        voto = int.Parse(input);

                        if (voto < 18) //messo per standard, se e' insufficiente associo lo 0
                            voto = 0;
                    }
                    break;
                case Esame.Fondamenti:
                case Esame.Programmazione1:
                case Esame.Programmazione2:
                    /*con il TryParse e' possibile provare a convertire la stringa in intero. Se non riesce `voto` resta a 0.
                    Dal momento che `input` non è numero solo quando si è insufficienti, ritirati o assenti e' ok*/
                    int.TryParse(input, out voto);
                    break;
                case Esame.Algoritmi:
                    if (input.Equals("30 LODE")) {
                        voto = 31;
                    } else {
                        int.TryParse(input, out voto);
                    }
                    break;
            }


            return voto;
        }
    }
}
