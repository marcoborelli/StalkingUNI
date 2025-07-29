package CSVManager

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Studente struct {
	Matricola string
	Cognome   string
	Nome      string
}

func GetNomeCognome(filepath, matricola string) (studente Studente) {
	studente.Matricola = matricola
	file, err := os.Open(filepath)

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()

		fields := strings.Split(line, ";")

		if fields[0] == matricola {
			studente.Cognome = fields[1]
			studente.Nome = fields[2]
			break
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println(err)
	}

	return
}

func GetMatricola(filepath, cognome, nome string) (studenti []Studente) {
	if cognome == "" && nome == "" {
		return
	}

	cognome = strings.ToUpper(cognome)
	nome = strings.ToUpper(nome)

	file, err := os.Open(filepath)

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()

		fields := strings.Split(line, ";")
		for i := 1; i < len(fields); i++ {
			fields[i] = strings.ToUpper(fields[i])
		}

		/*if cognome == fields[1] && nome == fields[2] ||
		cognome == fields[1] && nome == "" ||
		cognome == "" && nome == fields[2]*/

		if strings.Contains(fields[1], cognome) && strings.Contains(fields[2], nome) {
			studenti = append(studenti, Studente{Matricola: fields[0], Cognome: fields[1], Nome: fields[2]})
		}
	}

	if err := scanner.Err(); err != nil {
		fmt.Println(err)
	}

	return
}

func GetVoti(dirpath, matricola string) (voti map[string]int) {
	data, err := (os.ReadDir(dirpath)) //la cartella letta contiene sottocartelle, ognuna per ogni materia
	if err != nil {
		fmt.Println(err)
	}

	voti = make(map[string]int)

	for _, elem := range data {
		//ogni sottocaretella contiene un file .csv con lo stesso nome della directory in cui e' contenuto
		file, err := os.Open(dirpath + elem.Name() + string(os.PathSeparator) + elem.Name() + ".csv")

		if err != nil {
			fmt.Println(err)
		}

		defer file.Close()

		scanner := bufio.NewScanner(file)

		for scanner.Scan() {
			line := scanner.Text()

			fields := strings.Split(line, ";")

			if fields[0] == matricola {
				ris, err := strconv.Atoi(fields[1])
				if err != nil {
					fmt.Println(err)
					voti[elem.Name()] = -1
				} else {
					voti[elem.Name()] = ris
				}
				break
			}
		}

		if err := scanner.Err(); err != nil {
			fmt.Println(err)
		} else if _, ok := voti[elem.Name()]; !ok {
			//se sono qui non esiste la chiave -> l'esame non e' stato sostenuto
			voti[elem.Name()] = -1
		}
	}

	return
}
