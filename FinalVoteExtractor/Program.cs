using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace FinalVoteExtractor {
    class MainClass {
        public enum ProgParam {
            InputFilename,
            Count
        }

        public enum CSVField {
            Matricola,
            Voto,
            Count
        }

        public enum CSVFieldVoto {
            Data,
            TipoAppello, // C = completo, P = parziale, R = recupero (del parziale)
            Voto,
            Count
        }

        const char CSVFieldSeparator = ';';
        const char CSVVoteSeparator = ',';
        const char CSVVoteFieldSeparator = '_';

        public static void Main(string[] args) {
            string[] materie = new string[] {
                "AlgebraLineare",
                "Algoritmi",
                "Analisi1",
                "Architettura",
                "Fondamenti",
                "Programmazione1",
                "Programmazione2"
            };

            if (args.Length != (int)ProgParam.Count) {
                Console.Write("Error. Usage: ./programma ");

                //Array.ForEach(Enum.GetNames(typeof(ProgParam)), arg => Console.Write($"<{arg}> "));
                for (byte i = 0; i < (byte)ProgParam.Count; i++) {
                    Console.Write($"<{Enum.GetName(typeof(ProgParam), i)}> ");
                }
                Console.WriteLine("\n");

                throw new Exception("Wrong number of parameters");
            }

            // input: materia_completo.csv, a me serve solo materia
            string materia = args[(int)ProgParam.InputFilename].Split('_')[0];

            if (!materie.Contains(materia)) {
                throw new Exception("Wrong input enum. Call the program without parameters to see the enum list");
            }


            if (!File.Exists(args[(int)ProgParam.InputFilename])) {
                throw new Exception("Wrong input file path");
            }

            Dictionary<string, string> mat_voto = new Dictionary<string, string>();


            string line = "";
            using (StreamReader sr = new StreamReader(args[(int)ProgParam.InputFilename])) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(CSVFieldSeparator);
                    string mat = fields[(int)CSVField.Matricola].PadLeft(6, '0');


                    string[] fieldsVoto = fields[(int)CSVField.Voto].Split(CSVVoteFieldSeparator);
                    string dataEsame = fieldsVoto[(int)CSVFieldVoto.Data];
                    string tipoAppello = fieldsVoto[(int)CSVFieldVoto.TipoAppello];
                    string votoEsame = fieldsVoto[(int)CSVFieldVoto.Voto];


                    int voto = GetVoto(votoEsame, materia);


                    if (mat_voto.ContainsKey(mat))
                        mat_voto[mat] += CSVVoteSeparator;
                    else
                        mat_voto[mat] = "";

                    mat_voto[mat] += $"{dataEsame}{CSVVoteFieldSeparator}{tipoAppello}{CSVVoteFieldSeparator}{voto}";
                }
            }

            //Console.WriteLine(mat_voto[928561]);


            using (StreamWriter sw = new StreamWriter($"{materia}.csv")) {
                foreach (KeyValuePair<string, string> kvp in mat_voto) {
                    sw.WriteLine($"{kvp.Key}{CSVFieldSeparator}{kvp.Value}");
                }
            }


        }

        public static int GetVoto(string input, string materia) {
            int voto = 0;

            switch(materia) {
                case "Analisi1":
                    Regex rg = new Regex(@".+\s(?<votoInTrentesimi>\d+).+\s(?<suTrenta>\d)");
                    Match match = rg.Match(input);

                    if (rg.IsMatch(input)) {
                        voto = int.Parse(match.Groups["votoInTrentesimi"].Value);
                    }

                    break;
                case "AlgebraLineare":
                    if (input.Equals("30L")) {
                        voto = 31;
                    } else {
                        voto = int.Parse(input);

                        if (voto < 18) { // messo per standard, se e' insufficiente associo lo 0
                            voto = 0;
                        }
                    }
                    break;
                case "Fondamenti":
                case "Programmazione1":
                case "Programmazione2":
                case "Architettura":
                    /* se TryParse fallisce -> voto = 0.
                    Dato che `input` non è numero solo quando si è insufficienti, ritirati o assenti e' ok */
                    int.TryParse(input, out voto);
                    break;
                case "Algoritmi":
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
