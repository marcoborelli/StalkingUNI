using System;
using System.IO;
using System.Collections.Generic;

namespace StudentsMerge {
    class MainClass {
        public enum Params {
            OrigFilename,
            NewDataFilename,
            Count
        }

        public static void Main(string[] args) {
            if (args.Length != (int)Params.Count) {
                Console.Write($"Error. Usage: ./programma ");
                for (byte i = 0; i < (byte)Params.Count; i++) {
                    Console.Write($"<{Enum.GetName(typeof(Params), i)}> ");
                }
                Console.Write("\n");


                throw new Exception("Wrong number of parameters");
            }


            if (!File.Exists(args[(int)Params.NewDataFilename])) {
                throw new Exception("Wrong new file path");
            }

            //se non esiste il file originale lo si crea da zero, restituisco solo un warning
            if (!File.Exists(args[(int)Params.OrigFilename])) {
                Console.WriteLine($"Il file {args[(int)Params.OrigFilename]} non esiste. Ne verra' creato uno nuovo");
                File.Create(args[(int)Params.OrigFilename]).Close(); //close in quanto senno' resterebbe aperto uno stream creando poi eccezione
            }

            Dictionary<int, string> mat_cognomeNome = new Dictionary<int, string>();


            //salvo in un dizionario tutti i nuovi valori del file
            string line = "";
            using (StreamReader sr = new StreamReader(args[(int)Params.NewDataFilename])) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);

                    mat_cognomeNome[mat] = $"{fields[1]};{fields[2]}";
                }
            }


            //rimuovo dal dizionario i valori che gia' esistono all'interno del file originale
            using (StreamReader sr = new StreamReader(args[(int)Params.OrigFilename])) {
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);

                    //se la chiave non esiste nella collezione, la funziona ritorna emplicemnte false
                    mat_cognomeNome.Remove(mat);
                }
            }


            //i valori restanti nel dizionario sono quelli da aggiungere in append al file originale
            using (StreamWriter sw = new StreamWriter(args[(int)Params.OrigFilename], true)) {
                foreach (KeyValuePair<int, string> kvp in mat_cognomeNome) {
                    sw.WriteLine($"{kvp.Key};{kvp.Value}");
                }
            }
        }
    }
}
