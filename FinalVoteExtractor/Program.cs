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
            ArchitetturaElaboratori,
            Count
        }

        public enum ProgParam {
            InputFilename,
            OutputFilename,
            ExameTypeNumber,
            Count
        }

        const char CSVFieldSeparator = ';';
        const char CSVVoteSeparator = ',';
        const char CSVVoteFieldSeparator = '_';

        public static void Main(string[] args) {
            if (args.Length != (int)ProgParam.Count) {
                Console.Write("Error. Usage: ./programma ");
                for (byte i = 0; i < (byte)ProgParam.Count; i++) {
                    Console.Write($"<{Enum.GetName(typeof(ProgParam), i)}> ");
                }
                Console.WriteLine("\n");

                Console.WriteLine("The ExameType are: ");
                for (int i = 0; i < (int)Esame.Count; i++) {
                    Console.WriteLine($"{i}: {Enum.GetName(typeof(Esame), i)}");
                }

                throw new Exception("Wrong number of parameters");
            }


            Esame materiaEsame;

            if (int.TryParse(args[(int)ProgParam.ExameTypeNumber], out int num) && Enum.IsDefined(typeof(Esame), num)) {
                    materiaEsame = (Esame)num;
            } else {
                throw new Exception("Wrong input enum. Call the program without parameters to see the enum list");
            }


            if (!File.Exists(args[(int)ProgParam.InputFilename])) {
                throw new Exception("Wrong input file path");
            }

            Dictionary<int, string> mat_voto = new Dictionary<int, string>();


            string line = "";
            using (StreamReader sr = new StreamReader(args[(int)ProgParam.InputFilename])) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(CSVFieldSeparator);
                    int mat = int.Parse(fields[0]);

                    //GG-MM-AAAA_TIPOESAME_VOTO
                    string[] fieldsVoto = fields[1].Split(CSVVoteFieldSeparator);
                    string dataEsame = fieldsVoto[0];
                    string tipoAppello = fieldsVoto[1];
                    string votoEsame = fieldsVoto[2];


                    int voto = GetVoto(votoEsame, materiaEsame);


                    if (mat_voto.ContainsKey(mat))
                        mat_voto[mat] += CSVVoteSeparator;
                    else
                        mat_voto[mat] = "";

                    mat_voto[mat] += $"{dataEsame}{CSVVoteFieldSeparator}{tipoAppello}{CSVVoteFieldSeparator}{voto}";
                }
            }

            //Console.WriteLine(mat_voto[928561]);


            using (StreamWriter sw = new StreamWriter(args[(int)ProgParam.OutputFilename])) {
                foreach (KeyValuePair<int, string> kvp in mat_voto) {
                    sw.WriteLine($"{kvp.Key}{CSVFieldSeparator}{kvp.Value}");
                }
            }


        }

        public static int GetVoto(string input, Esame tipoEsame) {
            int voto = 0;

            switch(tipoEsame) {
                case Esame.Analisi1:
                    Regex rg = new Regex(@".+\s(?<votoInTrentesimi>\d+).+\s(?<suTrenta>\d)");
                    Match match = rg.Match(input);

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
                case Esame.ArchitetturaElaboratori:
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
