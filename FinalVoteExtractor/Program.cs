using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinalVoteExtractor {
    class MainClass {
        public static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("Error. Usage: ./programma <InputFilename> <outputFilename>");
                throw new Exception("Wrong parameters");
            }

            string InputFilename = args[0];
            string outputFilename = args[1];

            Dictionary<int, int> mat_voto = new Dictionary<int, int>();


            string line = "";
            using (StreamReader sr = new StreamReader(InputFilename)) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);
                    int voto = GetVoto(fields[1]);

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

        public static int GetVoto(string input) {
            int voto = 0;
            Regex rg = new Regex(@".+\s(?<votoInTrentesimi>\d+).+\s(?<suTrenta>\d)");
            Match match = rg.Match(input);

            if (rg.IsMatch(input)) {
                voto = int.Parse(match.Groups["votoInTrentesimi"].Value);
            }

            return voto;
        }
    }
}
