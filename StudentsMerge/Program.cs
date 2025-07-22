using System;
using System.IO;
using System.Collections.Generic;

namespace StudentsMerge {
    class MainClass {
        public static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("Error. Usage: ./programma <OrigFilename> <NewDataFilename>");
                throw new Exception("Wrong number of parameters");
            }


            if (!File.Exists(args[1])) {
                throw new Exception("Wrong input file path");
            }

            //se non esiste il file originale lo si crea da zero, restituisco solo un warning
            if (!File.Exists(args[0])) {
                Console.WriteLine($"Il file {args[0]} non esiste. Ne verra' creato uno nuovo");
                File.Create(args[0]).Close(); //close in quanto senno' resterebbe aperto uno stream creando poi eccezione
            }

            Dictionary<int, string> mat_cognomeNome = new Dictionary<int, string>();


            //salvo in un dizionario tutti i nuovi valori del file
            string line = "";
            using (StreamReader sr = new StreamReader(args[1])) {
                sr.ReadLine(); //salto l'intestazione
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);

                    mat_cognomeNome[mat] = $"{fields[1]};{fields[2]}";
                }
            }


            //rimuovo dal dizionario i valori che gia' esistono all'interno del file originale
            using (StreamReader sr = new StreamReader(args[0])) {
                while ((line = sr.ReadLine()) != null) {
                    string[] fields = line.Split(';');
                    int mat = int.Parse(fields[0]);

                    //se la chiave non esiste nella collezione, la funziona ritorna emplicemnte false
                    mat_cognomeNome.Remove(mat);
                }
            }


            //i valori restanti nel dizionario sono quelli da aggiungere in append al file originale
            using (StreamWriter sw = new StreamWriter(args[0], true)) {
                foreach (KeyValuePair<int, string> kvp in mat_cognomeNome) {
                    sw.WriteLine($"{kvp.Key};{kvp.Value}");
                }
            }
        }
    }
}
