using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace FinalVoteExtractor {
    class MainClass {
        public enum ProgParam {
            InputFolder,
            Count
        }

        public enum CSVField {
            Matricola,
            Voto,
            Count
        }

        public enum FilenameField {
            Materia,
            Data,
            TipoAppello, // C = completo, P = parziale, R = recupero (del parziale)
            Count
        }

        const char CSVFieldSeparator = ';';
        const char CSVVoteSeparator = ',';
        const char CSVVoteFieldSeparator = '_';

        static string[] materie = new string[] {
                "AlgebraLineare",
                "Algoritmi",
                "Analisi1",
                "Architettura",
                "Fondamenti",
                "Programmazione1",
                "Programmazione2",
                "MetodiAlgebrici",
                "SistemiReti"
                };

        const string APPELLI_FOLDER = "SingoliAppelli";

        public static void Main(string[] args) {
            if (args.Length != (int)ProgParam.Count) {
                Console.Write("Error. Usage: ./programma ");

                //Array.ForEach(Enum.GetNames(typeof(ProgParam)), arg => Console.Write($"<{arg}> "));
                for (byte i = 0; i < (byte)ProgParam.Count; i++) {
                    Console.Write($"<{Enum.GetName(typeof(ProgParam), i)}> ");
                }
                Console.WriteLine("\n");

                throw new Exception("Wrong number of parameters");
            }

            string dir_path = args[(int)ProgParam.InputFolder];

            List<string> file_paths = IsDirValid(dir_path);
            if (file_paths is null) {
                throw new Exception("La cartella fornita in input non è valida.");
            }


            Dictionary<string, string> mat_voto = new Dictionary<string, string>();

            string line = "";
            string materia = new DirectoryInfo(dir_path).Name;
            IVotoStrategy strategy = SFactory.GetInstance().GetVotoStrategy(materia);

            foreach (string file_path in file_paths) {
                // Replace() senno' nel tipo_appello c'e' anche l'estensione
                string file_name = Path.GetFileName(file_path).Replace(".csv", "");

                string[] filename_fields = file_name.Split('_');
                string data_esame = filename_fields[(int)FilenameField.Data];
                string tipo_appello = filename_fields[(int)FilenameField.TipoAppello];

                using (StreamReader sr = new StreamReader(file_path)) {
                    while ((line = sr.ReadLine()) != null) {
                        string[] fields = line.Split(CSVFieldSeparator);
                        string mat = fields[(int)CSVField.Matricola].PadLeft(6, '0');
                        string votoEsame = fields[(int)CSVField.Voto];

                        int voto = strategy.GetVoto(votoEsame);


                        if (mat_voto.ContainsKey(mat)) {
                            mat_voto[mat] += CSVVoteSeparator;
                        } else {
                            mat_voto[mat] = "";
                        }
                        mat_voto[mat] += $"{data_esame}{CSVVoteFieldSeparator}{tipo_appello}{CSVVoteFieldSeparator}{voto}";
                    }
                }
            }


            //Console.WriteLine(mat_voto[928561]);

            string path_dest = Path.Combine(dir_path, $"{materia}.csv");
            using (StreamWriter sw = new StreamWriter(path_dest)) {
                foreach (KeyValuePair<string, string> kvp in mat_voto) {
                    sw.WriteLine($"{kvp.Key}{CSVFieldSeparator}{kvp.Value}");
                }
            }

        }


        // Null -> la cartella fornita non e' valida
        // List<string> contiene i file dei singoli appelli (se la cartella e' valida)
        public static List<string> IsDirValid(string dir_path) {
            string nome_materia = new DirectoryInfo(dir_path).Name;
            string path_appelli = Path.Combine(dir_path, APPELLI_FOLDER);

            // nome cartella non e' una materia o non esiste sottocartella degli appelli
            if (!materie.Contains(nome_materia) || !Directory.Exists(path_appelli)) {
                return null;
            }

            // controllo che i file siano nominati correttamente
            List<string> files = new List<string>(Directory.EnumerateFiles(path_appelli));
            Regex rg = new Regex(nome_materia + @"_\d{4}-\d{2}-\d{2}_(P|C|R)\.csv"); // $ non posso metterlo perche' con {2} esplode
            bool res = files.All((file) => rg.IsMatch(Path.GetFileName(file))); // file contiene il percorso completo, a me serve solo il nome

            files = RadixSort(files);

            return res ? files : null;
        }

        // A parita' di data l'ordine e': P, R, C
        public static List<string> RadixSort(List<string> input) {
            // Ordino per gli ultimi 5 caratteri <TipoEsame>.csv
            // Da C# 8.0 file[^5..]
            List<string> tmp = input.OrderBy(file => GetPriority(file.Substring(file.Length - 5))).ToList();

            // Questo algoritmo e' stabile, Sort() non lo e'
            // Da C# 8.0 file[..^5]
            List<string> fin = tmp.OrderBy(file => file.Substring(0, file.Length - 5)).ToList();

            return fin;
        }

        public static byte GetPriority(string input) {
            byte res = 3;

            switch(input) {
                case "P.csv":
                    res = 0;
                    break;
                case "R.csv":
                    res = 1;
                    break;
                case "C.csv":
                    res = 2;
                    break;
            }

            return res;
        }

    }
}
